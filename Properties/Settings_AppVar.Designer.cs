﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OLKI.Programme.QuBC.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.5.0.0")]
    internal sealed partial class Settings_AppVar : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings_AppVar defaultInstance = ((Settings_AppVar)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings_AppVar())));
        
        public static Settings_AppVar Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0, 0")]
        [global::System.Configuration.SettingsManageabilityAttribute(global::System.Configuration.SettingsManageability.Roaming)]
        public global::System.Drawing.Size MainForm_Size {
            get {
                return ((global::System.Drawing.Size)(this["MainForm_Size"]));
            }
            set {
                this["MainForm_Size"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Normal")]
        [global::System.Configuration.SettingsManageabilityAttribute(global::System.Configuration.SettingsManageability.Roaming)]
        public global::System.Windows.Forms.FormWindowState MainForm_State {
            get {
                return ((global::System.Windows.Forms.FormWindowState)(this["MainForm_State"]));
            }
            set {
                this["MainForm_State"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        [global::System.Configuration.SettingsManageabilityAttribute(global::System.Configuration.SettingsManageability.Roaming)]
        public string RecentFiles_FileList {
            get {
                return ((string)(this["RecentFiles_FileList"]));
            }
            set {
                this["RecentFiles_FileList"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        [global::System.Configuration.SettingsManageabilityAttribute(global::System.Configuration.SettingsManageability.Roaming)]
        public bool SettingsUpgradet {
            get {
                return ((bool)(this["SettingsUpgradet"]));
            }
            set {
                this["SettingsUpgradet"] = value;
            }
        }
    }
}
