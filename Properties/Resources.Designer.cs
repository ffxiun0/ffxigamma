﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ffxigamma.Properties {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ffxigamma.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   厳密に型指定されたこのリソース クラスを使用して、すべての検索リソースに対し、
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   管理者モード に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string AdminMode {
            get {
                return ResourceManager.GetString("AdminMode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   既に起動しています。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string AlreadyRunning {
            get {
                return ResourceManager.GetString("AlreadyRunning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   FINAL FANTASY XI の起動に失敗しました。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string FFXIStartFail {
            get {
                return ResourceManager.GetString("FFXIStartFail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   (アイコン) に類似した型 System.Drawing.Icon のローカライズされたリソースを検索します。
        /// </summary>
        internal static System.Drawing.Icon Icon {
            get {
                object obj = ResourceManager.GetObject("Icon", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   画像を保存しました。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NotifySaved {
            get {
                return ResourceManager.GetString("NotifySaved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   リモートコントロールに失敗しました。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string RemoteControlFail {
            get {
                return ResourceManager.GetString("RemoteControlFail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   この機能を有効にするには管理者モードで起動する必要があります。
        ///
        /// FINAL FANTASY XI を終了し、コンテキストメニューより
        ///「FINAL FANTASY XI を起動」または「管理者モードで再起動」を
        ///実行することで使用できるようになります。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string RequireAdminWarning {
            get {
                return ResourceManager.GetString("RequireAdminWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   管理者モードで再起動を実行すると
        ///FINAL FANTASY XI の画面を喪失する可能性があります。
        ///再起動してもよろしいですか？ に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string RestartAdminModeWarning {
            get {
                return ResourceManager.GetString("RestartAdminModeWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   再起動に失敗しました。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string RestartFail {
            get {
                return ResourceManager.GetString("RestartFail", resourceCulture);
            }
        }
    }
}
