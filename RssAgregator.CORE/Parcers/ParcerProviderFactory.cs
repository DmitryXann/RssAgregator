using RssAgregator.CORE.Interfaces.Parcers;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace RssAgregator.CORE.Parcers
{
    public static class ParcerProviderFactory //: IParcerFactoryProviders
    {
        private static Dictionary<DataSourceEnum, IResourceParcer> _factoties = new Dictionary<DataSourceEnum, IResourceParcer>();

        static ParcerProviderFactory()
        {
            using (var db = new RssAggregatorModelContainer())
            {
                foreach (var dataSource in db.GetDBSet<DataSources>())
                {
                    if (!string.IsNullOrEmpty(dataSource.XMLGuide))
                    {
                        try
                        {
                            var xmlGuide = XDocument.Parse(dataSource.XMLGuide);

                            switch (dataSource.Type)
                            {
                                case DataSourceEnum.VK:
                                    AddFactory(dataSource.Type, typeof(VKResourceParcer), new[] { xmlGuide });
                                    break;
                                case DataSourceEnum.Pikabu:
                                    AddFactory(dataSource.Type, typeof(PikabuResourceParcer), new[] { xmlGuide });
                                    break;
                                case DataSourceEnum.Mainfun:
                                    AddFactory(dataSource.Type, typeof(MainfunResourceParcer), new[] { xmlGuide });
                                    break;
                                case DataSourceEnum.Zaycev:
                                    AddFactory(dataSource.Type, typeof(ZaycevResourceParcer), new[] { xmlGuide });
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.LogException(ex, LogTypeEnum.CORE);
                        }
                    }
                }
            }
        }

        //public static void AddFactory(DataSourceEnum factoryType, IResourceParcer factoryEntity)
        //{
        //    if (!_factoties.ContainsKey(factoryType))
        //    {
        //        _factoties.Add(factoryType, factoryEntity);
        //    }
        //}

        private static void AddFactory(DataSourceEnum factoryType, Type typeOfFactoryEntity, object[] parameters = null)
        {
            if (typeOfFactoryEntity.GetInterfaces().Any(el => el == typeof(IResourceParcer)))
            {
                var typeConstructor = typeOfFactoryEntity.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(XDocument) }, null);
                var typeInstance = typeConstructor.Invoke(parameters) as IResourceParcer;

                if (typeInstance != null && !_factoties.ContainsKey(factoryType))
                {
                    _factoties.Add(factoryType, typeInstance);
                }
            }
            else
            {
                throw new ArgumentException(string.Format("Invalid type, expected IResourceParcer, found: {0}", typeOfFactoryEntity));
            }

        }

        //public static void RemoveFactory(DataSourceEnum factoryType)
        //{
        //    if (_factoties.ContainsKey(factoryType))
        //    {
        //        _factoties.Remove(factoryType);
        //    }
        //}

        public static IResourceParcer GetFactory(DataSourceEnum factoryType)
        {
            return _factoties[factoryType];
        }

        public static void DisposeAllFactories()
        {
            foreach (var factory in _factoties)
            {
                factory.Value.Dispose();
            }

            _factoties.Clear();
        }
    }
}
