using RssAgregator.CORE.Interfaces.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers;
using RssAgregator.CORE.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RssAgregator.CORE.Parcers.XMLGuidePostModelParcer.XMLGuidePostModelParcers
{
    public static class XMLPostModelParcerFactory
    {
        private static Dictionary<XMLGuidePostModelParcersEnum, IXMLGuidePostModelParcer> _factoties = new Dictionary<XMLGuidePostModelParcersEnum, IXMLGuidePostModelParcer>();

        static XMLPostModelParcerFactory()
        {
            AddFactory(XMLGuidePostModelParcersEnum.AuthorId, typeof(AuthorIdXMLParcer));
            AddFactory(XMLGuidePostModelParcersEnum.PostId, typeof(PostIdXMLParcer));
            AddFactory(XMLGuidePostModelParcersEnum.PostLikes, typeof(PostLikesXMLParcer));
            AddFactory(XMLGuidePostModelParcersEnum.AuthorName, typeof(AuthorNameXMLParcer));
            AddFactory(XMLGuidePostModelParcersEnum.AuthorLink, typeof(AuthorLinkXMLParcer));
            AddFactory(XMLGuidePostModelParcersEnum.TextContent, typeof(TextContentXMLParcer));
            AddFactory(XMLGuidePostModelParcersEnum.ImgContent, typeof(ImgContentXMLParcer));
            AddFactory(XMLGuidePostModelParcersEnum.AudioContent, typeof(AudioContentXMLParcer));
            AddFactory(XMLGuidePostModelParcersEnum.PostName, typeof(PostNameXMLParcer));
            AddFactory(XMLGuidePostModelParcersEnum.PostLink, typeof(PostLinkXMLParcer));
            AddFactory(XMLGuidePostModelParcersEnum.VideoContent, typeof(VideoContentXMLParcer));
        }

        public static void AddFactory(XMLGuidePostModelParcersEnum factoryType, IXMLGuidePostModelParcer factoryEntity)
        {
            if (!_factoties.ContainsKey(factoryType))
            {
                _factoties.Add(factoryType, factoryEntity);
            }
        }

        public static void AddFactory(XMLGuidePostModelParcersEnum factoryType, Type typeOfFactoryEntity)
        {
            if (typeOfFactoryEntity.GetInterfaces().Any(el => el ==  typeof(IXMLGuidePostModelParcer)))
            {
                var typeConstructor = typeOfFactoryEntity.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[0], null);
                var typeInstance = typeConstructor.Invoke(null) as IXMLGuidePostModelParcer;

                if (typeInstance != null && !_factoties.ContainsKey(factoryType))
                {
                    _factoties.Add(factoryType, typeInstance);
                }
            }
            else
            {
                throw new ArgumentException(string.Format("Invalid type, expected IXMLGuidePostModelParcer, found: {0}", typeOfFactoryEntity));
            }
            
        }

        public static void RemoveFactory(XMLGuidePostModelParcersEnum factoryType)
        {
            if (_factoties.ContainsKey(factoryType))
            {
                _factoties.Remove(factoryType);
            }
        }

        public static IXMLGuidePostModelParcer GetFactory(XMLGuidePostModelParcersEnum factoryType)
        {
            return _factoties[factoryType];
        }
    }
}
