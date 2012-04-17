using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.MapEditor.Animations;

namespace Mindstep.EasterEgg.MapEditor
{
    public class TextureManager
    {
        public List<Texture2DWithPos> textures = new List<Texture2DWithPos>();
        public int Count { get { return textures.Count; } }


         
        public IEnumerable<Texture2DWithPos> FrontToBack()
        {
            for (int i = textures.Count - 1; i >= 0; i--)
            {
                yield return textures[i];
            }
        }
        public IEnumerable<Texture2DWithPos> BackToFront()
        {
            for (int i = 0; i < textures.Count; i++)
            {
                yield return textures[i];
            }
        }



        public void BringToFront(Texture2DWithPos tex)
        {
            Remove(tex);
            AddToFront(tex);
        }
        public void BringToFront(IEnumerable<Texture2DWithPos> texs)
        {
            foreach (Texture2DWithPos tex in texs)
            {
                BringToFront(tex);
            }
        }



        public void SendToBack(IEnumerable<Texture2DWithPos> texs)
        {
            foreach (Texture2DWithPos tex in texs)
            {
                SendToBack(tex);
            }
        }
        public void SendToBack(Texture2DWithPos tex)
        {
            Remove(tex);
            AddToBack(tex);
        }



        public void SendBackward(IEnumerable<Texture2DWithPos> texs)
        {
            foreach (Texture2DWithPos tex in texs)
            {
                SendBackward(tex);
            }
        }
        public void SendBackward(Texture2DWithPos tex)
        {
            int index = textures.LastIndexOf(tex);
            if (index != 0)
            {
                Texture2DWithPos otherTex = textures[index - 1];
                textures[index - 1] = tex;
                textures[index] = otherTex;
            }
        }



        public void BringForward(IEnumerable<Texture2DWithPos> texs)
        {
            foreach (Texture2DWithPos tex in texs)
            {
                BringForward(tex);
            }
        }
        public void BringForward(Texture2DWithPos tex)
        {
            int index = textures.IndexOf(tex);

            if (index + 1 != textures.Count)
            {
                Texture2DWithPos otherTex = textures[index + 1];
                textures[index + 1] = tex;
                textures[index] = otherTex;
            }
        }



        public void AddToBack(IEnumerable<Texture2DWithPos> texs)
        {
            foreach (Texture2DWithPos tex in texs)
            {
                AddToBack(tex);
            }
        }
        public void AddToBack(Texture2DWithPos tex)
        {
            textures.Insert(0, tex);
        }



        public void AddToFront(IEnumerable<Texture2DWithPos> texs)
        {
            foreach (Texture2DWithPos tex in texs)
            {
                AddToFront(tex);
            }
        }
        public void AddToFront(Texture2DWithPos tex)
        {
            textures.Add(tex);
        }



        public void Remove(IEnumerable<Texture2DWithPos> texs)
        {
            foreach (Texture2DWithPos tex in texs)
            {
                Remove(tex);
            }
        }
        public void Remove(Texture2DWithPos tex)
        {
            textures.Remove(tex);
        }
    }
}
