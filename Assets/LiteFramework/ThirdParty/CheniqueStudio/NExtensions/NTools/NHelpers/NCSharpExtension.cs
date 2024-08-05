namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NCSharpExtension
    {
        public static bool Toggle(this ref bool b)
        {
            b = !b; //toggle job is done.
            return b; //extra return value allows it being used inside expression.
        }
    }
}