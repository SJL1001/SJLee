using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace SJLee
{
    internal class Global : IDisposable
    {
        private static readonly Lazy<Global> _instance = new Lazy<Global>(() => new Global());

        public static Global Inst
        {
            get
            {
                return _instance.Value;
            }
        }

        private InspStage _stage = new InspStage();

        public InspStage InspStage
        {
            get { return _stage; }
        }
         public Global()
        {
        }

        public void Initialize()
        {
            _stage.Initialize();
        }
        public void Dispose()
        {
            _stage.Dispose();
        }
    }
}
