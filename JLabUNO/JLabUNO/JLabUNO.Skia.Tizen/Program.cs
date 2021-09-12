using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace JLabUNO.Skia.Tizen
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new TizenHost(() => new JLabUNO.App(), args);
            host.Run();
        }
    }
}
