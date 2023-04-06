using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pub.Core.Common {
    public enum EventStatus {
        active,
        occured,
        deleted,
        draft
    }

    public enum TeamAssignmentStatus {
        active,
        accepted,
        rejected,
        deleted
    }
}
