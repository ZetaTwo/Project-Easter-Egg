using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public class ImageManager<T> where T : ImageWithPos
    {
        public List<T> textures = new List<T>();
        public int Count { get { return textures.Count; } }


         
        public IEnumerable<T> FrontToBack()
        {
            for (int i = textures.Count - 1; i >= 0; i--)
            {
                yield return textures[i];
            }
        }
        public IEnumerable<T> BackToFront()
        {
            for (int i = 0; i < textures.Count; i++)
            {
                yield return textures[i];
            }
        }



        public void BringToFront(T tex)
        {
            Remove(tex);
            AddToFront(tex);
        }
        public void BringToFront(IEnumerable<T> texs)
        {
            foreach (T tex in texs)
            {
                BringToFront(tex);
            }
        }



        public void SendToBack(IEnumerable<T> texs)
        {
            foreach (T tex in texs)
            {
                SendToBack(tex);
            }
        }
        public void SendToBack(T tex)
        {
            Remove(tex);
            AddToBack(tex);
        }



        public void SendBackward(IEnumerable<T> texs)
        {
            foreach (T tex in texs)
            {
                SendBackward(tex);
            }
        }
        public void SendBackward(T tex)
        {
            int index = textures.LastIndexOf(tex);

            textures.Remove(tex);
            for (index--; index >= 0; index--)
            {
                if (textures[index].Bounds.Intersects(tex.Bounds))
                {
                    textures.Insert(index, tex);
                    break;
                }
            }
            AddToBack(tex);
        }



        public void BringForward(IEnumerable<T> texs)
        {
            foreach (T tex in texs)
            {
                BringForward(tex);
            }
        }
        public void BringForward(T tex)
        {
            int index = textures.IndexOf(tex);

            textures.Remove(tex);
            for (; index < textures.Count; index++)
            {
                if (textures[index].Bounds.Intersects(tex.Bounds))
                {
                    textures.Insert(index+1, tex);
                    break;
                }
            }
            AddToFront(tex);
        }



        public void AddToBack(IEnumerable<T> texs)
        {
            foreach (T tex in texs)
            {
                AddToBack(tex);
            }
        }
        public void AddToBack(T tex)
        {
            textures.Insert(0, tex);
        }



        public void AddToFront(IEnumerable<T> texs)
        {
            foreach (T tex in texs)
            {
                AddToFront(tex);
            }
        }
        public void AddToFront(T tex)
        {
            textures.Add(tex);
        }



        public void Remove(IEnumerable<T> texs)
        {
            foreach (T tex in texs)
            {
                Remove(tex);
            }
        }
        public void Remove(T tex)
        {
            textures.Remove(tex);
        }
    }
}
