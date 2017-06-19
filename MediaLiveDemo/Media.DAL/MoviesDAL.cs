/**  版本信息模板在安装目录下，可自行修改。
* Movies.cs
*
* 功 能： N/A
* 类 名： Movies
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/6/19 18:50:36   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Media.IDAL;
using Media.Model;
using System.Collections.Generic;
using Media.Infrastructure;
using Dapper;
using System.Linq;

namespace Media.SQLServerDAL
{
    /// <summary>
    /// 数据访问类:Movies
    /// </summary>
    public partial class MoviesDAL : IMoviesDAL
    {
        private string connString = @"Data Source=.;Initial Catalog=Movies;User ID=sa;Password=123";
        private DBType dbtype = DBType.MainDB;
        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Model.Movies GetEntity(string id)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                const string query = "select * from Movies where MovieID=@id";
                return conn.Query<Movies>(query, new { id = id })
                                .SingleOrDefault<Movies>();
            }

        }

        public int Insert(IEnumerable<Model.Movies> entities)
        {
            throw new NotImplementedException();
        }

        public int Insert(Model.Movies entity)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                string query = @"INSERT INTO	dbo.Movies
        (CategoriesID,
          MovieName,
          MovieDirector,
          MovieActor,
          MovieDesc,
          MovieData,
          MovieTime,
          MovieHit,
          AddDate,
          MovieCountry,
          MovieLanguage,
          MovieImage,
          MoviePath
        )
VALUES( @CategoriesID,@MovieName,@MovieDirector,@MovieActor,@MovieDesc,@MovieData,@MovieTime,@MovieHit,@AddDate,@MovieCountry,@MovieLanguage,@MovieImage,@MoviePath)";
                int row = conn.Execute(query, entity);
                //更新对象的Id为数据库里新增的Id,假如增加之后不需要获得新增的对象，
                //只需将对象添加到数据库里，可以将下面的一行注释掉。
                SetIdentity(conn, id => entity.MovieID = id, "MovieID", "Movies");
                return row;

            }
        }

        public IEnumerable<Movies> QueryAll()
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                const string query = "select * from Movies ";
                return conn.Query<Movies>(query);
            }
        }

        public int Update(Movies entity)
        {
            throw new NotImplementedException();
        }

        public void SetIdentity(IDbConnection conn, Action<int> setId, string primarykey
                          , string tableName)
        {
            if (string.IsNullOrEmpty(primarykey)) primarykey = "id";
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("tableName参数不能为空，为查询的表名");
            }
            string query = string.Format("SELECT max({0}) as Id FROM {1}", primarykey
                                 , tableName);
            NewId identity = conn.Query<NewId>(query, null).Single<NewId>();
            setId(identity.Id);
        }

        public class NewId
        {
            public int Id { get; set; }
        }
    }
}

