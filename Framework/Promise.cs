using System;

namespace Rivel.Framework
{
    internal class Promise
    {

        Action Resolve { get; set; }

        public Promise(Action resolve)
        {
            this.Resolve = resolve;
            resolve();
        }

        public Promise then(Action resolve)
        {
            return new Promise(resolve);
        }



    }
}
