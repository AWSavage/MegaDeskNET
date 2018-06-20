namespace MegaDesk
{
    struct Desk
    {
        public enum DesktopSurfaceMaterial
        {
            Laminate,
            Oak,
            Rosewood,
            Veneer,
            Pine
        }

        public int Width { get; set; }
        public int Depth { get; set; }
        public int NumberOfDrawers { get; set; }
        public DesktopSurfaceMaterial SurfaceMaterial { get; set; }
    }
}
