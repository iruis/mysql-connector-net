﻿// Copyright © 2016, Oracle and/or its affiliates. All rights reserved.
//
// MySQL Connector/NET is licensed under the terms of the GPLv2
// <http://www.gnu.org/licenses/old-licenses/gpl-2.0.html>, like most 
// MySQL Connectors. There are special exceptions to the terms and 
// conditions of the GPLv2 as it is applied to this software, see the 
// FLOSS License Exception
// <http://www.mysql.com/about/legal/licensing/foss-exception.html>.
//
// This program is free software; you can redistribute it and/or modify 
// it under the terms of the GNU General Public License as published 
// by the Free Software Foundation; version 2 of the License.
//
// This program is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
// or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License 
// for more details.
//
// You should have received a copy of the GNU General Public License along 
// with this program; if not, write to the Free Software Foundation, Inc., 
// 51 Franklin St, Fifth Floor, Boston, MA 02110-1301  USA

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MySql.Data.MySqlClient.Test
{
  public class DataReader : Common
  {
    [Fact]
    public void ReadDataReader()
    {
      CheckAppSettings();

      //Test the same result using two differents connections to the same db and table, should be the same
      MySqlConnection connection1 = new MySqlConnection();
      MySqlConnection connection2 = new MySqlConnection();

      connection1.Open();
      connection2.Open();

      MySqlCommand command1 = new MySqlCommand("SELECT * FROM sakila.category;", connection1);
      MySqlCommand command2 = new MySqlCommand("SELECT * FROM sakila.category;", connection2);

      using (MySqlDataReader reader1 = command1.ExecuteReader())
      {
        using (MySqlDataReader reader2 = command2.ExecuteReader())
        {
          while (true)
          {
            if (!reader1.Read() && !reader2.Read())
            {
              reader2.Read();
              Assert.Equal(reader1, reader2);
            }
            else
            {
              break;
            }
          }
        }
      }

      connection1.Close();
      connection2.Close();
    }
  }
}
