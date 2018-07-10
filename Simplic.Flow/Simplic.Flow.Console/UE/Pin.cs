using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Console.UE
{
    public class Pin
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ToolTip { get; set; }
        public UE.PinDirection Direction { get; set; }
        public PinType PinType { get; set; }
        public string DefaultValue { get; set; }
        public object DefaultObject { get; set; }
        public IList<Pin> LinkedTo { get; set; }
        public IList<Pin> SubPins { get; set; }
        public Pin ParentPin { get; set; }
        public void MakeLinkTo(Pin ToPin) {

        }
        public void BreakLinkToU(Pin ToPin) { }
        public void BreakAllPinLinks(bool shouldNotifyNodes = false) { }
        
    }
}
