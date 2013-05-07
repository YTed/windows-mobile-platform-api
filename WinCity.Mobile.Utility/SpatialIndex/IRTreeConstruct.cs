using System;
using System.Collections.Generic;
using System.Text;

namespace WinCity.Mobile.Utility.SpatialIndex
{
	public interface IRTreeConstruct
	{
		IRTree Construct(IEnumerable<IRTreeDataAdapter> e);
	}

	public interface IRTreeDataAdapter
	{
		void Envelope(out double xMax, out double xMin, 
			out double yMax, out double yMin);
		object Hook { get; }
	}
}
