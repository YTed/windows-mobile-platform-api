using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace WinCity.Mobile.Utility.SpatialIndex
{
	public class RTree : IRTree, IRTreeConstruct
	{
		public RTree(int capacity)
		{
			if (capacity <= 1)
			{
				throw new ArgumentException("扇必须大于1");
			}
			this.capacity = capacity;
			lowlimit = capacity >> 1;
			
			root = new InternalRTreeNode(capacity);
		}

		#region IRTree 成员

		public int Capacity
		{
			get { return capacity; }
		}

		public int Count
		{
			get { return count; }
		}

		public void Add(IRTreeNode node)
		{
			IInternalRTreeNode selectedNode = root;
			root.Insert(node);
			count++;
		}

		public void AddRange(IEnumerable<IRTreeNode> nodes)
		{
			foreach (IRTreeNode node in nodes)
			{
				Add(node);
			}
		}

		public bool Remove(IRTreeNode node)
		{
			bool success = root.Remove(node);
			if(success)
				count--;
			return success;
		}

		public void Clear()
		{
			root.RemoveAll();
			count = 0;
		}

		[Obsolete]
		public IRTreeNode[] HitTest(
			double xMax, double xMin, double yMax, double yMin)
		{
			IEnvelope envelope = new Envelope();
			envelope.PutCoordinate(xMax, xMin, yMax, yMin);
			return root.HitTest(envelope);
		}

		[Obsolete]
		public IRTreeNode[] HitTest(double x, double y)
		{
			return HitTest(x, x, y, y);
		}

		public IEnumerable<IRTreeNode> Query(
			double xMax, double xMin, double yMax, double yMin)
		{
			IRTreeNode[] nodes = HitTest(xMax, xMin, yMax, yMin);
			if (nodes.Length == 0)
			{
				return nodes;
			}
			InternalRTreeNode[] internalNodes = new InternalRTreeNode[nodes.Length];
			Array.Copy(nodes, internalNodes, nodes.Length);
			return new RTreeNodeEnumerator(internalNodes);
		}

		public double XMax
		{
			get { return root.Envelope.XMax; }
		}

		public double XMin
		{
			get { return root.Envelope.XMin; }
		}

		public double YMax
		{
			get { return root.Envelope.YMax; }
		}

		public double YMin
		{
			get { return root.Envelope.YMin; }
		}

		#endregion

		#region IRTreeConstruct 成员

		public IRTree Construct(IEnumerable<IRTreeDataAdapter> e)
		{
			List<InternalRTreeNode> nodeList = new List<InternalRTreeNode>();
			foreach (IRTreeDataAdapter rtda in e)
			{
				InternalRTreeNode node = new InternalRTreeNode(capacity, rtda.Hook);
				double xmax, xmin, ymax, ymin;
				rtda.Envelope(out xmax, out xmin, out ymax, out ymin);
				node.PutCoordinate(xmax, xmin, ymax, ymin);
				nodeList.Add(node);
			}
			foreach (IRTreeNode node in this)
			{
				nodeList.Add(node as InternalRTreeNode);
			}
			Clear();
			count = nodeList.Count;
			(root as IInternalRTreeNodeConstruct).Construct(nodeList, true);
			return this;
		}

		#endregion
		
		#region IEnumerable<IRTreeNode> 成员

		public IEnumerator<IRTreeNode> GetEnumerator()
		{
			return new RTreeNodeEnumerator(root as InternalRTreeNode);
		}

		#endregion

		#region IEnumerable 成员

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		private class RTreeNodeEnumerator : IEnumerator<IRTreeNode>, IEnumerable<IRTreeNode>
		{
			public RTreeNodeEnumerator(InternalRTreeNode[] nodes)
			{
				if (nodes == null || nodes.Length == 0)
				{
					throw new ArgumentException();
				}
				else
				{
					for (int i = 0; i < nodes.Length; i++)
					{
						if (nodes[i] == null)
						{
							throw new ArgumentException();
						}
					}
					this.nodes = nodes;
					this.version = nodes[0].Version;
				}
				nodeStack = new Stack<InternalRTreeNode>();
				posStack = new Stack<int>();
				this.isDisposed = false;
				Reset();
			}

			public RTreeNodeEnumerator(InternalRTreeNode root)
				: this(new InternalRTreeNode[] { root })
			{
			}
			
			#region IEnumerator<IRTreeNode> 成员

			public IRTreeNode Current
			{
				get { return current; }
			}

			#endregion

			#region IDisposable 成员

			public void Dispose()
			{
				nodes = null;
				isDisposed = true;
			}

			#endregion

			#region IEnumerator 成员

			object IEnumerator.Current
			{
				get
				{
					CheckDisposed();
					CheckVersion();
					return current;
				}
			}

			public bool MoveNext()
			{
				CheckDisposed();
				CheckVersion();
				current = null;
				int currentPos = -1;
				while (nodeStack.Count > 0 && current == null)
				{
					current = nodeStack.Pop();
					currentPos = posStack.Pop();
					currentPos++;
					bool notFound = true;
					if (current.IsLeaf)
					{
						notFound = false;
					}
					else if (current.Children.Length > currentPos)
					{
						nodeStack.Push(current);
						posStack.Push(currentPos);
						InternalRTreeNode child = current.Children[currentPos];
						if (child != null)
						{
							if (child.IsLeaf)
							{
								current = child;
								notFound = false;
							}
							else
							{
								nodeStack.Push(child);
								posStack.Push(-1);
							}
						}
					}
					if (notFound)
					{
						current = null;
					}
				}
				return current != null;
			}

			public void Reset()
			{
				CheckDisposed();
				CheckVersion();

				nodeStack.Clear();
				posStack.Clear();
				foreach (InternalRTreeNode node in nodes)
				{
					if (node != null)
					{
						nodeStack.Push(node);
						posStack.Push(-1);
					}
				}
			}

			#endregion

			#region IEnumerable<IRTreeNode> 成员

			public IEnumerator<IRTreeNode> GetEnumerator()
			{
				return this;
			}

			#endregion

			#region IEnumerable 成员

			IEnumerator IEnumerable.GetEnumerator()
			{
				return this;
			}

			#endregion

			private void SetRoot(InternalRTreeNode root)
			{
			}

			private void CheckVersion()
			{
				if (nodes[0] != null && nodes[0].Version != version)
				{
					throw new NotSupportedException("由于容器结构已发生变化, 枚举器不能再在此容器中使用");
				}
			}

			private void CheckDisposed()
			{
				if (isDisposed)
				{
					throw new NotSupportedException("枚举器已被释放");
				}
			}

			private InternalRTreeNode[] nodes;
			
			private Stack<InternalRTreeNode> nodeStack;
			private Stack<int> posStack;
			private InternalRTreeNode current;
			private int version;
			private bool isDisposed;

		}


		private int capacity, lowlimit;
		private int count;

		private IInternalRTreeNode root;

	}

	interface IInternalRTreeNode : IRTreeNode
	{
		IEnvelope Envelope { get; }
		int Level { get; }

		IInternalRTreeNode Insert(IRTreeNode node);
		bool Contains(IRTreeNode node);
		bool Remove(IRTreeNode node);
		void RemoveAll();
		IInternalRTreeNode Break();

		IRTreeNode[] HitTest(IEnvelope envelope);
	}

	interface IInternalRTreeNodeConstruct
	{
		void Construct(List<InternalRTreeNode> nodes, bool alongX);
	}

	interface IEnvelope
	{
		double XMax { get; set; }
		double XMin { get; set; }
		double YMax { get; set; }
		double YMin { get; set; }

		double Area { get; }

		void PutCoordinate(double xmax, double xmin,
			double ymax, double ymin);

		bool Intersect(IEnvelope other);
		bool Intersect(double xmax, double xmin, double ymax, double ymin);
		double IntersectArea(IEnvelope other);
		bool Contains(IEnvelope other);
		bool Contains(double xmax, double xmin, double ymax, double ymin);
		void Union(IEnvelope other, IEnvelope result);
		
	}

	class Envelope : IEnvelope
	{
		#region IEnvelope 成员

		public double XMax
		{
			get
			{
				return xmax;
			}
			set
			{
				xmax = value;
			}
		}

		public double XMin
		{
			get
			{
				return xmin;
			}
			set
			{
				xmin = value;
			}
		}

		public double YMax
		{
			get
			{
				return ymax;
			}
			set
			{
				ymax = value;
			}
		}

		public double YMin
		{
			get
			{
				return ymin;
			}
			set
			{
				ymin = value;
			}
		}

		public double Area
		{
			get
			{
				return Math.Abs(xmax - xmin) * Math.Abs(ymax - ymin);
			}
		}

		public bool Intersect(IEnvelope other)
		{
			return Intersect(other.XMax, other.XMin,
				other.YMax, other.YMin);
		}

		public bool Intersect(double xmax, double xmin,
			double ymax, double ymin)
		{
			return (this.xmax >= xmin) &&
				(xmax >= this.xmin) &&
				(this.ymax >= ymin) &&
				(ymax >= this.ymin);
		}

		public double IntersectArea(IEnvelope other)
		{
			if (Intersect(other))
			{
				double xmax = Math.Min(this.xmax, other.XMax);
				double xmin = Math.Max(this.xmin, other.XMin);
				double ymax = Math.Min(this.ymax, other.YMax);
				double ymin = Math.Max(this.ymin, other.YMin);
				return (xmax - xmin) * (ymax - ymin);
			}
			return 0;
		}

		public void Union(IEnvelope other, IEnvelope result)
		{
			result.XMax = Math.Max(xmax, other.XMax);
			result.XMin = Math.Min(xmin, other.XMin);
			result.YMax = Math.Max(ymax, other.YMax);
			result.YMin = Math.Min(ymin, other.YMin);
		}

		public void PutCoordinate(double xmax, double xmin,
			double ymax, double ymin)
		{
			this.xmax = Math.Max(xmax, xmin);
			this.xmin = Math.Min(xmax, xmin);
			this.ymax = Math.Max(ymax, ymin);
			this.ymin = Math.Min(ymax, ymin);
		}

		public bool Contains(IEnvelope other)
		{
			return Contains(other.XMax, other.XMin, other.YMax, other.YMin);
		}

		public bool Contains(double xmax, double xmin, double ymax, double ymin)
		{
			return (this.xmax >= xmax) &&
				(this.xmin <= xmin) &&
				(this.ymax >= ymax) &&
				(this.ymin <= ymin);
		}
		
		#endregion

		private double xmax, xmin, ymax, ymin;

	}

	class InternalRTreeNode : RTreeNode, IInternalRTreeNode, IInternalRTreeNodeConstruct
	{

		#region constructors
		
		public InternalRTreeNode(int capacity, int level)
			: this(capacity)
		{
			if (level > 2)
			{
				children[0] = new InternalRTreeNode(capacity, level - 1);
			}
		}

		public InternalRTreeNode(int capacity, object hook)
			: base(hook, true)
		{

		}
		
		public InternalRTreeNode(int capacity)
			: base(null, false)
		{
			if (capacity <= 0)
			{
				throw new ArgumentException();
			}
			children = new InternalRTreeNode[capacity + 1];
			lowerLimit = capacity >> 1;
		}

		private InternalRTreeNode(object hook, IInternalRTreeNode parent)
			: base(hook, true)
		{
			this.parent = parent;
		}

		#endregion

		#region IInternalRTreeNode 成员

		public int Level
		{
			get
			{
				if ((children == null) || (IsLeaf && (parent != null)))
				{
					return 1;
				}
				if (children[0] == null)
				{
					return 2;
				}
				return children[0].Level + 1;
			}
		}

		public IEnvelope Envelope
		{
			get { return envelope; }
		}

		public IInternalRTreeNode Insert(IRTreeNode node)
		{
			IncVersion();
			InternalRTreeNode result = null;
			int nodeLevel = node is IInternalRTreeNode ?
				(node as IInternalRTreeNode).Level : 1;
			if ((Level - nodeLevel) == 1 )
			{
				result = InsertChild(node);
			}
			else
			{
				InsertGrandchild(node);
			}
			if (count == Capacity)
			{
				IRTreeNode breakNode = Break();
				if (parent != null && breakNode != null)
				{
					parent.Insert(breakNode);
				}
			}
			return result;
		}

		public bool Contains(IRTreeNode node)
		{
			if (!IsLeaf)
			{
				bool intersect = envelope.Intersect(
					node.XMax, node.XMin, node.YMax, node.YMin);
				if (intersect)
				{
					for (int i = 0; i < count; i++)
					{
						if (children[i].Contains(node))
						{
							return true;
						}
					}
				}
				return false;
			}
			else
			{
				return Equals(node);
			}
		}

		public bool Remove(IRTreeNode node)
		{
			if(node == null)
			{
				throw new ArgumentNullException();
			}
			IncVersion();
			bool success = false;
			if (Level != 2)
			{
				for (int i = 0; i < count; i++)
				{
					InternalRTreeNode tmpChild = children[i];
					if (tmpChild.Envelope.Intersect(
						node.XMax, node.XMin, node.YMax, node.YMin))
					{
						success |= tmpChild.Remove(node);
					}
				}
				Adjust();
			}
			else
			{
				for (int i = count - 1; i >= 0; i--)
				{
					if (children[i].Equals(node))
					{
						RemoveAt(i);
						success = true;
					}
				}
			}
			return success;
		}

		public void RemoveAll()
		{
			IncVersion();
			for (int i = 0; i < children.Length; i++)
			{
				children[i] = null;
			}
			count = 0;
			envelope.PutCoordinate(
				double.MinValue, double.MinValue, double.MinValue, double.MinValue);
		}

		public IRTreeNode[] HitTest(IEnvelope envelope)
		{
			bool intersect = envelope.Intersect(this.envelope);
			if (IsLeaf && intersect)
			{
				return new IRTreeNode[] { this };
			}
			
			if (envelope.Contains(this.envelope))
			{
				IRTreeNode[] result = new IRTreeNode[count];
				Array.Copy(children, 0, result, 0, count);
				return result;
			}
			else if(intersect)
			{
				List<IRTreeNode> tmpList = new List<IRTreeNode>();
				for (int i = 0; i < count; i++)
				{
					IInternalRTreeNode subnode = children[i];
					IRTreeNode[] hitNodes = subnode.HitTest(envelope);
					tmpList.AddRange(hitNodes);
				}
				return tmpList.ToArray();
			}
			return new IRTreeNode[] { };
		}

		public IInternalRTreeNode Break()
		{
			if (parent == null)
			{
				return BreakRoot();
			}
			else
			{
				return BreakNormal();
			}
		}

		private IInternalRTreeNode BreakRoot()
		{
			int fan = Capacity;
			InternalRTreeNode piece = BreakNormal() as InternalRTreeNode;
			InternalRTreeNode otherPiece = new InternalRTreeNode(fan);
			InternalRTreeNode[] subnodes = otherPiece.children;
			for (int i = 0; i < count; i++)
			{
				children[i].parent = otherPiece;
			}
			otherPiece.children = children;
			otherPiece.count = count;
			otherPiece.IsLeaf = false;
			children = subnodes;

			piece.UpdateEnvelope();
			otherPiece.UpdateEnvelope();

			subnodes[0] = piece;
			subnodes[1] = otherPiece;
			otherPiece.parent = this;
			count = 2;
			return null;
		}

		private IInternalRTreeNode BreakNormal()
		{
			int sep1, sep2;
			int[] sepArr1 = new int[count];
			int[] sepArr2 = new int[count];
			for (int i = 0; i < count; i++)
			{
				sepArr1[i] = -1;
				sepArr2[i] = -1;
			}
			IEnvelope tmpEnv = new Envelope();
			Break_FindShouldSeperate(tmpEnv, out sep1, out sep2);
			Break_SeperateTwo(tmpEnv, sep1, sep2, sepArr1, sepArr2);

			InternalRTreeNode result = new InternalRTreeNode(Capacity);
			result.parent = this.parent;
			for (int i = 0; (i < sepArr1.Length) && (sepArr1[i] >= 0); i++)
			{
				InternalRTreeNode child = children[sepArr1[i]];
				child.parent = result;
				result.children[i] = child;
				result.count++;
			}
			result.UpdateEnvelope();
			InternalRTreeNode[] newChildren = new InternalRTreeNode[children.Length];
			count = 0;
			for (int i = 0; (i < sepArr2.Length) && (sepArr2[i] >= 0); i++)
			{
				newChildren[i] = children[sepArr2[i]];
				count++;
			}
			UpdateEnvelope();
			children = newChildren;
			return result;
		}

        // core
		private void Break_FindShouldSeperate(IEnvelope union, out int sep1, out int sep2)
		{
			sep1 = -1;
			sep2 = -1;
			double maxDist = double.MinValue;
			for (int i = 0; i < count; i++)
			{
				IEnvelope env1 = children[i].envelope;
				for (int j = i + 1; j < count; j++)
				{
					IEnvelope env2 = children[j].envelope;
					double tmpDist = Distance(env1, env2, union);
					if (tmpDist > maxDist)
					{
						sep1 = i;
						sep2 = j;
						maxDist = tmpDist;
					}
				}
			}
		}

		private void Break_SeperateTwo(IEnvelope union, int sep1, int sep2, int[] arr1, int[] arr2)
		{
			IEnvelope env1 = children[sep1].envelope;
			IEnvelope env2 = children[sep2].envelope;
			IEnvelope union1 = new Envelope(), union2 = new Envelope();
			union1.PutCoordinate(envelope.XMax, envelope.XMin, envelope.YMax, envelope.YMin);
			union2.PutCoordinate(envelope.XMax, envelope.XMin, envelope.YMax, envelope.YMin);
			arr1[0] = sep1;
			arr2[0] = sep2;
			int arr1Count = 1, arr2Count = 1;

			for (int i = 0; i < count; i++)
			{
				if ((i != sep1) && (i != sep2))
				{
					IEnvelope env = children[i].envelope;
					double deltArea1 = UnionDeltaArea(env1, env, union);
					double deltArea2 = UnionDeltaArea(env2, env, union);
					if (deltArea1 > deltArea2)
					{
						arr1[arr1Count++] = i;
						union1.Union(env, union1);
					}
					else
					{
						arr2[arr2Count++] = i;
						union2.Union(env, union2);
					}
				}
			}
		}

		private double Distance(IEnvelope env1, IEnvelope env2, IEnvelope union)
		{
			env1.Union(env2, union);
			double deltaX = union.XMax - union.XMin;
			double deltaY = union.YMax - union.YMin;
			return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
		}

		private double UnionDeltaArea(IEnvelope env1, IEnvelope env2, IEnvelope union)
		{
			env1.Union(env2, union);
			return union.Area - (env1.Area + env2.Area) + env1.IntersectArea(env2);
		}

		private void UpdateEnvelope(params int[] list)
		{
			if (list.Length == 0)
			{
				IEnvelope env0 = children[0].envelope;
				envelope.PutCoordinate(env0.XMax, env0.XMin, env0.YMax, env0.YMin);
				for (int i = 1; i < children.Length; i++)
				{
					if (children[i] != null)
					{
						IEnvelope envi = children[i].envelope;
						envelope.Union(envi, envelope);
					}
				}
			}
			else
			{
				foreach (int index in list)
				{
					IInternalRTreeNode node = children[index];
					IEnvelope env = node.Envelope;
					envelope.Union(env, envelope);
				}
			}
		}

		private void Adjust()
		{
			for(int i = count-1; i>=0 ; i--)
			{
				InternalRTreeNode childNode = children[i];
				if (childNode.children.Length == 0)
				{
					RemoveAt(i);
				}
			}
			UpdateEnvelope();
		}

		private void RemoveAt(int index)
		{
			InternalRTreeNode node = children[index];
			node.parent = null;
			for (int j = count - 1; j > index; j--)
			{
				children[j - 1] = children[j];
			}
			count--;
		}
		
		#endregion

		#region IInternalRTreeNodeConstruct 成员

		public void Construct(List<InternalRTreeNode> nodes, bool alongX)
		{
			if (nodes.Count <= Capacity)
			{
				for (int i = 0; i < nodes.Count; i++)
				{
					children[i] = nodes[i];
					children[i].parent = this;
				}
				count = nodes.Count;
			}
			else
			{
				IComparer<InternalRTreeNode> comparer = null;
				if (alongX)
				{
					comparer = new XComparer();
				}
				else
				{
					comparer = new YComparer();
				}
				nodes.Sort(comparer);
				int q = nodes.Count / Capacity;
				int r = nodes.Count % Capacity;
				int index = 0;
				for (int i = 0; i < Capacity; i++)
				{
					int num = q;
					num += r > 0 ? 1 : 0;
					r--;
					List<InternalRTreeNode> subnodes = nodes.GetRange(index, num);
					children[i] = new InternalRTreeNode(Capacity);
					children[i].parent = this;
					(children[i] as IInternalRTreeNodeConstruct).Construct(subnodes, !alongX);
					index += num;
				}
				count = Capacity;
			}
			UpdateEnvelope();
		}

		#endregion

		internal InternalRTreeNode[] Children
		{
			get { return children; }
		}

		internal int Version
		{
			get { return version; }
		}

		private InternalRTreeNode InsertChild(IRTreeNode node)
		{
			InternalRTreeNode result = null;
			if (Level == 2)
			{
				object hook = node.Hook;
				result = new InternalRTreeNode(hook, this);
				result.Envelope.PutCoordinate(
					node.XMax, node.XMin, node.YMax, node.YMin);
			}
			else
			{
				result = node as InternalRTreeNode;
			}
			children[count] = result;
			UpdateEnvelope(count);
			count++;
			return result;
		}

		private InternalRTreeNode InsertGrandchild(IRTreeNode node)
		{
			InternalRTreeNode result = null;
			InternalRTreeNode child;
			int index;
			SelectChild(node, out index, out child);
			if (child != null)
			{
				result = child.Insert(node) as InternalRTreeNode;
				if (result != null && result.parent == this)
				{
					if (index != -1)
					{
						UpdateEnvelope(index);
					}
				}
				else
				{
					UpdateEnvelope();
				}
			}
			return result;
		}

		private void SelectChild(IRTreeNode node,
			out int index, out InternalRTreeNode child)
		{
			IEnvelope unionEnvelope = new Envelope();
			IEnvelope otherEnvelope = new Envelope();
			otherEnvelope.PutCoordinate(
				node.XMax, node.XMin, node.YMax, node.YMin);

			int fan = Capacity;
			index = -1;
			child = null;
			double miniInc = double.MaxValue;
			for (int i = 0; i < count; i++)
			{
				InternalRTreeNode tmpNode = children[i];
				IEnvelope tmpEnvelope = tmpNode.Envelope;
				double inc = UnionDeltaArea(
					otherEnvelope, tmpEnvelope, unionEnvelope);
				if (inc < miniInc)
				{
					child = tmpNode;
					index = i;
				}
			}
		}

		private void IncVersion()
		{
			version++;
		}

		private int Capacity
		{
			get
			{
				if (children != null)
				{
					return children.Length - 1;
				}
				return 0;
			}
		}
		
		private InternalRTreeNode[] children;
		private IInternalRTreeNode parent;
		private int count;
		private int lowerLimit;

		private int version;
	}


	class XComparer : IComparer<InternalRTreeNode>
	{
		#region IComparer<IInternalRTreeNode> 成员

		public int Compare(InternalRTreeNode x, InternalRTreeNode y)
		{
			// if R((x.XMax + x.XMin)/2, (y.XMax + y.XMin)/2) is true
			// we got R((x.XMax + x.XMin), (y.XMax + y.XMin)) is ture as well
			// where R may be =, > or <
			double xSum = x.XMax + x.XMin;
			double ySum = y.XMax + y.XMin;
			if (xSum.Equals(ySum))
			{
				return 0;
			}
			if (xSum > ySum)
			{
				return 1;
			}
			return -1;
		}

		#endregion
	}

	class YComparer : IComparer<InternalRTreeNode>
	{
		#region IComparer<IInternalRTreeNode> 成员

		public int Compare(InternalRTreeNode x, InternalRTreeNode y)
		{
			double xSum = x.YMax + x.YMin;
			double ySum = y.YMax + y.YMin;
			if (xSum.Equals(ySum))
			{
				return 0;
			}
			if (xSum > ySum)
			{
				return 1;
			}
			return -1;
		}

		#endregion
	}

}
