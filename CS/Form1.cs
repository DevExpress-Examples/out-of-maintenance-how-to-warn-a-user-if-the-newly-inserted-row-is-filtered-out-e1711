using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System.Collections;

namespace WarnNewRowDisappears {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            new DevExpress.XtraGrid.Design.XViewsPrinting(gridControl1);
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridView1.ActiveFilterCriteria = new OperandProperty("Discontinued") == new OperandValue(false);
        }

        bool IsRowFilteredOut(GridView view, object dataRow) {
            if(view == null || view.DataSource == null || dataRow == null)
                throw new ArgumentNullException();
            int dataSourceIndex = ((IList)view.DataSource).IndexOf(dataRow);
            if(dataSourceIndex < 0) 
                throw new Exception("Row doesn't belong to the data source");
            return view.GetRowHandle(dataSourceIndex) < 0;
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e) {
            GridView view = (GridView)sender;
            if(view.ActiveFilterEnabled && view.FocusedRowHandle == GridControl.NewItemRowHandle) {
                if(IsRowFilteredOut(view, e.Row))
                    MessageBox.Show("The new row you've just inserted is filtered out of the view due to the active filter.");
            }
        }
    }
}