using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.WidgetTemplates
{
    public class WidgetTemplatesCreateScreenFrame : WidgetTemplatesCreateEditBaseScreen
    {
        public WidgetTemplatesCreateScreenFrame(Find find)
            : base(find)
        {
        }

        public override FrameInfo GetHostedFrameInfo()
        {
            return new FrameInfo() { Name = "create" };
        }
    }
}
