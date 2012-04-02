using System.Windows.Forms;
using Microsoft.Xna.Framework.Content;

namespace MapEditor
{
    public partial class MapEditor1 : Form
    {
        private ContentManager Content;
        public MapEditor1()
        {
            InitializeComponent();
            LoadContent();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected void LoadContent()
        {
            Content = new ContentManager(new ServiceProvider());
            topView.LoadContent(Content);
        }
    }
}
