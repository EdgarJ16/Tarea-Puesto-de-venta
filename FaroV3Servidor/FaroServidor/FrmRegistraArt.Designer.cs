
namespace FaroServidor
{
    partial class FrmRegistraArt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.agregarArt = new System.Windows.Forms.Button();
            this.estadoArt = new System.Windows.Forms.ComboBox();
            this.txtPrecioVenta = new System.Windows.Forms.TextBox();
            this.precioFinal = new System.Windows.Forms.TextBox();
            this.cantidadArt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(281, -48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 21);
            this.label2.TabIndex = 10;
            this.label2.Text = "Catálogo";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(191, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 20);
            this.label6.TabIndex = 25;
            this.label6.Text = "Registre los artículos";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(392, 302);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Descripción";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(267, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Cantidad";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(268, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Precio Final";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(268, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Precio Venta";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Navy;
            this.label7.Location = new System.Drawing.Point(268, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Estado Actv/Inact";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(194, 300);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(193, 20);
            this.txtDescripcion.TabIndex = 19;
            // 
            // agregarArt
            // 
            this.agregarArt.BackColor = System.Drawing.Color.LightSeaGreen;
            this.agregarArt.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.agregarArt.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.agregarArt.Location = new System.Drawing.Point(195, 368);
            this.agregarArt.Name = "agregarArt";
            this.agregarArt.Size = new System.Drawing.Size(196, 60);
            this.agregarArt.TabIndex = 18;
            this.agregarArt.Text = "Agregar";
            this.agregarArt.UseVisualStyleBackColor = false;
            this.agregarArt.Click += new System.EventHandler(this.agregarArt_Click);
            // 
            // estadoArt
            // 
            this.estadoArt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.estadoArt.FormattingEnabled = true;
            this.estadoArt.Items.AddRange(new object[] {
            "Activo",
            "Inactivo"});
            this.estadoArt.Location = new System.Drawing.Point(194, 97);
            this.estadoArt.MaxDropDownItems = 2;
            this.estadoArt.Name = "estadoArt";
            this.estadoArt.Size = new System.Drawing.Size(69, 21);
            this.estadoArt.TabIndex = 15;
            // 
            // txtPrecioVenta
            // 
            this.txtPrecioVenta.Location = new System.Drawing.Point(195, 146);
            this.txtPrecioVenta.Name = "txtPrecioVenta";
            this.txtPrecioVenta.Size = new System.Drawing.Size(69, 20);
            this.txtPrecioVenta.TabIndex = 14;
            // 
            // precioFinal
            // 
            this.precioFinal.Location = new System.Drawing.Point(195, 197);
            this.precioFinal.Name = "precioFinal";
            this.precioFinal.Size = new System.Drawing.Size(69, 20);
            this.precioFinal.TabIndex = 16;
            // 
            // cantidadArt
            // 
            this.cantidadArt.Location = new System.Drawing.Point(194, 249);
            this.cantidadArt.Name = "cantidadArt";
            this.cantidadArt.Size = new System.Drawing.Size(69, 20);
            this.cantidadArt.TabIndex = 17;
            // 
            // FrmRegistraArt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(648, 471);
            this.ControlBox = false;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.agregarArt);
            this.Controls.Add(this.cantidadArt);
            this.Controls.Add(this.precioFinal);
            this.Controls.Add(this.estadoArt);
            this.Controls.Add(this.txtPrecioVenta);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRegistraArt";
            this.Text = "FrmVenta";
            this.Load += new System.EventHandler(this.FrmRegistraArt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button agregarArt;
        private System.Windows.Forms.ComboBox estadoArt;
        private System.Windows.Forms.TextBox txtPrecioVenta;
        private System.Windows.Forms.TextBox precioFinal;
        private System.Windows.Forms.TextBox cantidadArt;
    }
}