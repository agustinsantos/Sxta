namespace Sxta.GUIUtils
{
    using System;
    using System.Diagnostics;

    // Import log4net classes.
    using log4net;

    /// <summary>
    /// Summary description for TextBoxTraceListener.
    /// </summary>
    public class ListBoxTraceListener : TraceListener
    {
        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ListBoxTraceListener(System.Windows.Forms.ListBox lb)
        {
            listbox_ = lb;
        }

        public System.Windows.Forms.ListBox ListBox
        {
            get
            {
                return listbox_;
            }
            set
            {
                listbox_ = value;
            }
        }

        public override void Write(string Message)
        {
            try
            {
                internalbuffer_ = internalbuffer_ + Message;
                Process();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteLine(string Message)
        {
            try
            {
                this.Write(Message + "\n");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Clear()
        {
            if (listbox_.InvokeRequired)
            {
                ClearCallback d = new ClearCallback(this.Clear);
                listbox_.BeginInvoke(d, null);
            }
            else
            {
                listbox_.Items.Clear();
            }
        }

        delegate void SetTextCallback(string text);
        delegate void ClearCallback();


        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (listbox_.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(this.SetText);
                listbox_.BeginInvoke(d, new object[] { text });
            }
            else
            {
                listbox_.Items.Add(text);
            }
        }

        protected void Process()
        {
            foreach (string str in internalbuffer_.Split('\n'))
            {
                if (str.Length > 0)
                {
                    SetText(WriteIndentLevel() + str.Replace("\r", ""));
                }
            }
            internalbuffer_ = "";
        }

        protected string WriteIndentLevel()
        {
            return " ";
        }

        private System.Windows.Forms.ListBox listbox_;
        private string internalbuffer_;
    }
}
