using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerOldWorld
{
        public abstract class CustomSubModule
        {
            public CustomSubModule()
            {
                ModuleControllerSubModule.Instance.LoadCustomModule(this);
            }
            /// <summary>
            /// Called every frame, avoid using costly operations
            /// </summary>

            public abstract void OnUpdate();

            /// <summary>
            /// Used to initialize module
            /// </summary>
            public abstract void OnLoad();

            /// <summary>
            /// Override to clean up after the module stops being used
            /// </summary>
            public abstract void OnUnload();
        }

}
