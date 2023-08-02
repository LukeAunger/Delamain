using System;

namespace delamain_maui.Data
{
	public class detailsDatabase
	{
        SQLiteAsyncConnection Database;

        //method that when called checks if a database has been created and returns it. if the database hasn't been made it will that call the Constants to create
        // a new intsance of the database
        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<patient_call>();
        }

        //gets all items from the local storage
        public async Task<List<patient_call>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<patient_call>().ToListAsync();
        }

        //gets item where the integer passed into the parameter matches one in the database
        public async Task<patient_call> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<patient_call>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        //checks if the id matches any ids pre existing in the database if it does it will update the entry
        //if not then it will insert a new entry
        public async Task<int> SaveItemAsync(patient_call item)
        {
            await Init();
            if (item.Id != 0)
            {
                return await Database.UpdateAsync(item);
            }
            else
                return await Database.InsertAsync(item);
        }

        //To delete the entry you made
        public async Task<int> DeleteItemAsync(patient_call item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }

    }
}

