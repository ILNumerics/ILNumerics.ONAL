using ILNumerics.Core.DeviceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.MemoryLayer {
    /// <summary>
    /// This interface serves as the common API for all memory pools. It is used for maintenance of all pool types with a common interface. 
    /// </summary>
    public interface IMemoryPool {

        void Shrink(long maxLength);

        int ThreadID { get; }

        MemoryTypes MemoryType { get;  }

        ulong Size { get;  }

        Device Device { get; }

    }
}
