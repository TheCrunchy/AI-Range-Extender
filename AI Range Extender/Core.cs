using Sandbox.Game.Entities.Cube;
using Sandbox.Game.GameSystems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Torch;
using Torch.API;
using Torch.API.Managers;
using Torch.API.Plugins;
using Torch.API.Session;
using Torch.Managers;
using Torch.Managers.PatchManager;
using Torch.Session;

namespace AI_Range_Extender
{
    public class Core : TorchPluginBase
    {
        public static Logger Log = LogManager.GetLogger("AIRange");
        public override void Init(ITorchBase torch)
        {
            base.Init(torch);

            var sessionManager = Torch.Managers.GetManager<TorchSessionManager>();
            if (sessionManager != null)
            {
                sessionManager.SessionStateChanged += SessionChanged;
            }
            SetupConfig();
        }

        private void SetupConfig()
        {
            FileUtils utils = new FileUtils();

            if (File.Exists(StoragePath + "\\AIRange.xml"))
            {
                config = utils.ReadFromXmlFile<Config>(StoragePath + "\\AIRange.xml");
                utils.WriteToXmlFile<Config>(StoragePath + "\\AIRange.xml", config, false);
            }
            else
            {
                config = new Config();
                utils.WriteToXmlFile<Config>(StoragePath + "\\AIRange.xml", config, false);
            }

        }

        private void SessionChanged(ITorchSession session, TorchSessionState newState)
        {

        }
        public static Config config;
    }
}

