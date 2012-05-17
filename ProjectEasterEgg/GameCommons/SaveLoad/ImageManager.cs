using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
            foreach (T tex in texs.OrderBy(tex => textures.IndexOf(tex)))
            {
                SendBackward(tex);
            }
        }
        public void SendBackward(T tex)
        {
            T intersectingTex = textures.FirstOrDefault(t => t.Bounds.Intersects(tex.Bounds));
            if (intersectingTex != null &&
                textures.IndexOf(intersectingTex) < textures.IndexOf(tex))
            {
                Remove(tex);
                textures.Insert(textures.IndexOf(intersectingTex), tex);
            }
            else
            {
                Remove(tex);
                AddToBack(tex);
            }
        }



        public void BringForward(IEnumerable<T> texs)
        {
            foreach (T tex in texs.OrderByDescending(tex => textures.IndexOf(tex)))
            {
                BringForward(tex);
            }
        }
        public void BringForward(T tex)
        {
            T intersectingTex = textures.LastOrDefault(t => t.Bounds.Intersects(tex.Bounds));
            if (intersectingTex != null &&
                textures.IndexOf(intersectingTex) > textures.IndexOf(tex))
            {
                Remove(tex);
                textures.Insert(textures.IndexOf(intersectingTex) + 1, tex);
            }
            else
            {
                Remove(tex);
                AddToFront(tex);
            }
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
            renameTextureIfNecessary(tex);
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
            renameTextureIfNecessary(tex);
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
                    Match match = Regex.Match(tex.name, "^" + Regex.Escape(originalName) + @" \(\(d+)\)$");
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
