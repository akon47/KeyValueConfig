using System;
using System.Collections.Generic;
using System.Text;

namespace KeyValueConfig
{
    public interface IConfigurable
    {
        ConfigStore CreateConfigStore();
        void LoadFromConfigStore(ConfigStore configStore);
    }
}
