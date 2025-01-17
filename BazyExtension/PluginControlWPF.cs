﻿using System;
using System.ComponentModel;
using System.Windows.Controls;
using BazyExtension.App_Code;

namespace BazyExtension
{
    [TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<PluginControlWPF, UserControl>))]
    /// <summary>
    /// Main Bazy Plugin Manager class, each plugin need to inherit from this class.
    /// </summary>
    public abstract class PluginControlWPF : UserControl, IPluginControl
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        protected PluginControlWPF()
        {
        }

        /// <summary>
        /// Default constructor with parameters
        /// </summary>
        /// <param name="oLEDBConnectionString">Connection string</param>
        /// <param name="uILanguage">Language od aplication</param>
        /// <param name="table">Table of database</param>
        /// <param name="tabNo">Table NO</param>
        /// <param name="recNo"> RecNo</param>
        protected PluginControlWPF(string oLEDBConnectionString, string uILanguage, string table, int tabNo, int recNo)
        {
            OLEDBConnectionString = oLEDBConnectionString;
            UILanguage = uILanguage;
            this.table = table;
            this.tabNo = tabNo;
            this.recNo = recNo;
        }

        #endregion


        /// <summary>
        /// Call this method if you want to notify Bazy that data was changed in your plugin and Bazy need to refersh UI. Just call this method after you change data in Bazy plugin.<br/>
        /// For example:
        /// <example>
        /// <code>
        /// <para>
        /// private void button1_Click(object sender, EventArgs e) {<br/>
        /// //change data and save to SQL Database...<br/>
        /// // now notify Bazy<br/>
        /// DataChanged(null, null);<br/>
        /// }<br/><br/>
        /// </para>
        /// </code>
        /// </example>
        /// If you don't do this Bazy will not refresh UI (reload context data in Bazy).
        /// </summary>
        public abstract event EventHandler DataChanged;


        /// <summary>
        /// Set to action to invoke this on close plugin windows
        /// </summary>
        public event Action OnCloseMethod;


        /// <summary>
        /// Connection string provided by Bazy. This is OLE DB connection strong so to convert it to SQLClient Data Provider reference <c>Okna.Data.dll</c> to your plugin and using code in this example get new connection string.
        /// <code>
        /// <example>
        /// string[] str = Okna.Data.Utils.ModifyConnString(OLEDBConnectionString);<br/>
        /// string SqlConnestionStringFactory = str[0];<br/>
        /// </example>
        /// </code>
        /// </summary>
        public abstract string OLEDBConnectionString { set; get; }

        /// <summary>
        /// UI language set in Bazy. You can localize your plugin using this variable.
        /// <example>
        /// Example value: pl-PL
        /// </example>
        /// </summary>
        public abstract string UILanguage { set; get; }

        /// <summary>
        /// SQL Server table name. Each tab in Bazy can be releated with SQL table. This is table name from Bazy context.
        /// <example>
        /// Example value: KONSTRWZD
        /// </example>
        /// </summary>
        public abstract string table { set; get; }

        /// <summary>
        /// In Bazy this is zero based tab index for selected item. You can determine if plugin can be run for this tab or not.
        /// </summary>
        public abstract int tabNo { set; get; }

        /// <summary>
        /// Each SQL table has unique row index. This index is from Bazy context.
        /// </summary>
        public abstract int recNo { set; get; }

        /// <summary>
        /// Methos to invoke all actions from event <see cref="OnCloseMethod"/>
        /// </summary>
        public void CloseActions()
        {
            OnCloseMethod?.Invoke();
        }
    }
}
