using System;
using System.Collections.Generic;
using System.Text;

namespace WinCity.Mobile.Utility.SpatialIndex
{
	public class RTreeNode : IRTreeNode
	{
		public RTreeNode(object hook)
			: this(hook, true)
		{ }

		protected RTreeNode(object hook, bool isLeaf)
		{
			this.hook = hook;
			this.isLeaf = isLeaf;
			envelope = new Envelope();
		}
		
		#region IRTreeNode 成员

		public virtual double XMax
		{
			get
			{
				return envelope.XMax;
			}
		}

		public virtual double XMin
		{
			get
			{
				return envelope.XMin;
			}
		}

		public virtual double YMax
		{
			get
			{
				return envelope.YMax;
			}
		}

		public virtual double YMin
		{
			get
			{
				return envelope.YMin;
			}
		}

		public virtual bool IsLeaf
		{
			get { return isLeaf; }
			set { isLeaf = value; }
		}

		public virtual object Hook
		{
			get { return hook; }
			set { hook = value; }
		}

		public virtual void PutCoordinate(
			double xmax, double xmin, double ymax, double ymin)
		{
			envelope.PutCoordinate(xmax, xmin, ymax, ymin);
		}

		public bool Equals(IRTreeNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException();
			}
			return HookEqual(node.Hook) &&
				node.XMax.Equals(envelope.XMax) &&
				node.XMin.Equals(envelope.XMin) &&
				node.YMax.Equals(envelope.YMax) &&
				node.YMin.Equals(envelope.YMin);
		}

		private bool HookEqual(object hook)
		{
			bool valueEqual = hook == Hook;
			bool referenceEqual = (hook != null) &&
				(Hook != null) &&
				(hook.Equals(Hook));
			return valueEqual | referenceEqual;
		}

		#endregion

		internal IEnvelope envelope;

		private bool isLeaf;

		private object hook;
	}
}
