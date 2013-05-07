using System;
using System.Collections.Generic;
using System.Text;

namespace WinCity.Mobile.Utility.SpatialIndex
{
	/// <summary>
	/// 二维 R-树
	/// </summary>
	public interface IRTree : IEnumerable<IRTreeNode>, IContainer<IRTreeNode>
	{
		/// <summary>
		/// R 树的扇,为每个结点的最大子节点数
		/// </summary>
		int Capacity { get; }

		int Count { get; }

		/// <summary>
		/// 添加节点
		/// </summary>
		/// <param name="node"></param>
		void Add(IRTreeNode node);

		/// <summary>
		/// 批量插入
		/// </summary>
		/// <param name="nodes"></param>
		void AddRange(IEnumerable<IRTreeNode> nodes);

		/// <summary>
		/// 移除节点
		/// </summary>
		/// <param name="node"></param>
		bool Remove(IRTreeNode node);

		/// <summary>
		/// 清除
		/// </summary>
		void Clear();

		/// <summary>
		/// 命中测试
		/// </summary>
		/// <param name="xMax"></param>
		/// <param name="xMin"></param>
		/// <param name="yMax"></param>
		/// <param name="yMin"></param>
		/// <returns></returns>
		[Obsolete]
		IRTreeNode[] HitTest(
			double xMax, double xMin,
			double yMax, double yMin);

		/// <summary>
		/// 命中测试
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		[Obsolete]
		IRTreeNode[] HitTest(double x, double y);

		IEnumerable<IRTreeNode> Query(
			double xMax, double xMin,
			double yMax, double yMin);
		
		double XMax { get; }
		double XMin { get; }
		double YMax { get; }
		double YMin { get; }
	}

	/// <summary>
	/// R 树节点
	/// </summary>
	public interface IRTreeNode
	{
		double XMax { get; }
		double XMin { get; }
		double YMax { get; }
		double YMin { get; }

		void PutCoordinate(double xmax, double xmin, double ymax, double ymin);

		/// <summary>
		/// 是否为叶子
		/// </summary>
		bool IsLeaf { get; }
		/// <summary>
		/// 获取钩子
		/// </summary>
		object Hook { get; }

		bool Equals(IRTreeNode node);
	}

}
