using MorningFM.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MorningFM.Logic.Repository
{
    public class MorningFMRepository<T>: BaseRepository<T>
    {
        public MorningFMRepository(string connectionString, string database, string collection)
            :base(connectionString, database, collection)
        {

        }
    }
}
