using System.IO;
using System.Reflection;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using Service.SqlMaps;

namespace Service.BLL
{

    public class IBll
    {
        protected static ISqlMapper SqlMap = MyBatis.SqlMap;
    }
}
