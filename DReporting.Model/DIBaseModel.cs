using Autofac;
using DReporting.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DReporting.Model
{
    public class DIBaseModel : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BaseModel>().As<IBaseModel>();
        }
    }
}
