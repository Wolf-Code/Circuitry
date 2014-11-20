using System;

namespace SharpLib2D.Attributes
{
    [AttributeUsage( AttributeTargets.Property | AttributeTargets.Field )]
    public class UserSettingAttribute : Attribute
    {
        public string SettingName { private set; get; }

        public UserSettingAttribute( string Name )
        {
            this.SettingName = Name;
        }
    }

    [AttributeUsage( AttributeTargets.Class )]
    public class UserSettingContainerAttribute : Attribute
    {
        public string ContainerName
        {
            private set;
            get;
        }

        public UserSettingContainerAttribute( string Name )
        {
            this.ContainerName = Name;
        }
    }
}
