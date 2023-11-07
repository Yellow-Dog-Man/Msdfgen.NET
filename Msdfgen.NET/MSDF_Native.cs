using System;
using System.Runtime.InteropServices;

namespace Msdfgen.NET
{
    public static class MSDF_Native
    {
        const string LIBRARY_NAME = "msdfgen";

        [DllImport(LIBRARY_NAME)]
        public static extern IntPtr create_shape();

        [DllImport(LIBRARY_NAME)]
        public static extern void free_shape(IntPtr shape);

        [DllImport(LIBRARY_NAME)]
        public static extern void shape_edge_coloring_simple(IntPtr shape, double angleThreshold, ulong seed);

        [DllImport(LIBRARY_NAME)]
        public static extern void shape_bounds(IntPtr shape, [In][Out] ref double left, [In][Out] ref double bottom, [In][Out] ref double right, [In][Out] ref double top);

        [DllImport(LIBRARY_NAME)]
        public static extern IntPtr shape_add_contour(IntPtr shape);

        [DllImport(LIBRARY_NAME)]
        public static extern void contour_add_line(IntPtr contour, double fromX, double fromY, double toX, double toY);

        [DllImport(LIBRARY_NAME)]
        public static extern void contour_add_conic(IntPtr contour, double fromX, double fromY, double controlX, double controlY, double toX, double toY);

        [DllImport(LIBRARY_NAME)]
        public static extern void contour_add_cubic(IntPtr contour, double fromX, double fromY, double control0X, double control0Y, double control1X, double control1Y, double toX, double toY);

        [DllImport(LIBRARY_NAME)]
        public static extern void shape_generateMSDF(IntPtr pixels, int width, int height,
            IntPtr shape,
            double range, double scaleX, double scaleY, double offsetX, double offsetY,
            double edgeThreshold);
    }
}
