using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheOmenDen.Shared.Components.AudioDisplay.Internal
{
    public class AudioErrorEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32? SoundId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String? Message { get; set; }
    }
}
