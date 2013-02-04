using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public class ImageManager<T> : Modifiable
        where T : ImageWithPos
    {
        public event EventHandler Modified;
        private void Modify()
        {
            if (Modified != null) Modified(this, null);
        }

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







        public void BringToFront(IEnumerable<T> texs)
        {
            foreach (T tex in texs)
            {
                pBringToFront(tex);
            }
            Modify();
        }
        public void BringToFront(T tex)
        {
            pBringToFront(tex);
            Modify();
        }
        private void pBringToFront(T tex)
        {
            pRemove(tex);
            pAddToFront(tex);
        }



        public void SendToBack(IEnumerable<T> texs)
        {
            foreach (T tex in texs)
            {
                pSendToBack(tex);
            }
            Modify();
        }
        public void SendToBack(T tex)
        {
            pSendToBack(tex);
            Modify();
        }
        private void pSendToBack(T tex)
        {
            pRemove(tex);
            pAddToBack(tex);
        }



        public void SendBackward(IEnumerable<T> texs)
        {
            foreach (T tex in texs.OrderBy(tex => textures.IndexOf(tex)))
            {
                pSendBackward(tex);
            }
            Modify();
        }
        public void SendBackward(T tex)
        {
            pSendBackward(tex);
            Modify();
        }
        private void pSendBackward(T tex)
        {
            T intersectingTex = textures.FirstOrDefault(t => t.Bounds.Intersects(tex.Bounds));
            if (intersectingTex != null &&
                textures.IndexOf(intersectingTex) < textures.IndexOf(tex))
            {
                pRemove(tex);
                textures.Insert(textures.IndexOf(intersectingTex), tex);
            }
            else
            {
                pRemove(tex);
                pAddToBack(tex);
            }
        }



        public void BringForward(IEnumerable<T> texs)
        {
            foreach (T tex in texs.OrderByDescending(tex => textures.IndexOf(tex)))
            {
                pBringForward(tex);
            }
            Modify();
        }
        public void BringForward(T tex)
        {
            pBringForward(tex);
            Modify();
        }
        private void pBringForward(T tex)
        {
            T intersectingTex = textures.LastOrDefault(t => t.Bounds.Intersects(tex.Bounds));
            if (intersectingTex != null &&
                textures.IndexOf(intersectingTex) > textures.IndexOf(tex))
            {
                pRemove(tex);
                textures.Insert(textures.IndexOf(intersectingTex) + 1, tex);
            }
            else
            {
                pRemove(tex);
                pAddToFront(tex);
            }
        }



        public void AddToBack(IEnumerable<T> texs)
        {
            foreach (T tex in texs)
            {
                renameTextureIfNecessary(tex);
                tex.Modified += Modified;
                pAddToBack(tex);
            }
            Modify();
        }
        public void AddToBack(T tex)
        {
            AddToBack(new T[] { tex });
        }
        private void pAddToBack(T tex)
        {
            textures.Insert(0, tex);
        }



        public void AddToFront(IEnumerable<T> texs)
        {
            foreach (T tex in texs)
            {
                renameTextureIfNecessary(tex);
                tex.Modified += Modified;
                pAddToFront(tex);
            }
            Modify();
        }
        public void AddToFront(T tex)
        {
            AddToFront(new T[]{tex});
        }
        private void pAddToFront(T tex)
        {
            textures.Add(tex);
        }



        public void Remove(IEnumerable<T> texs)
        {
            foreach (T tex in texs)
            {
                pRemove(tex);
                tex.Modified -= Modified;
            }
            Modify();
        }
        public void Remove(T tex)
        {
            pRemove(tex);
            tex.Modified -= Modified;
            Modify();
        }
        private void pRemove(T tex)
        {
            textures.Remove(tex);
        }





        //TODO: should this method be placed somewhere else? in the model manager for example
        private void renameTextureIfNecessary(T tex)
        {
            if (textures.Any(t => t.name == tex.name))
            {
                string originalName;
                {
                    Match match = Regex.Match(tex.name, @"^(.*) \(\d+\)$");
                    if (match.Success)
                    {
                        originalName = match.Captures[0].Value;
                    }
                    else
                    {
                        originalName = tex.name;
                    }
                }
                int lastIndex = 2;
                foreach (T t in textures)
                {
                    Match match = Regex.Match(tex.name, "^" + Regex.Escape(originalName) + @" \((d+)\)$");
                    if (match.Success)
                    {
                        lastIndex = Math.Max(lastIndex, int.Parse(match.Captures[0].Value));
                    }
                }

                tex.name = originalName + " (" + lastIndex + ")";
            }
        }
    }
}
