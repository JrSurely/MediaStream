
using Media.Infrastructure;
using Media.Model;
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
namespace Media.IDAL
{
	/// <summary>
	/// 接口层Movies
	/// </summary>
	public interface IMoviesDAL:IRepository<Movies>
	{
	} 
}
