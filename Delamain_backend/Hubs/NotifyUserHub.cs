using System;

namespace Delamain_backend.Hubs
{
	public class NotifyUserHub : Hub
	{
		private readonly DataContext _context;
		public NotifyUserHub(DataContext context)
		{
			_context = context;
		}
		private static Dictionary<string, string> patients = new Dictionary<string, string>();

		public override async Task OnConnectedAsync()
		{
            //var users = Mockdata.initialuserrequest.ToList();
            string key = Context.GetHttpContext().Request.Query["key"];
            patients.Add(Context.ConnectionId, key);
			await SendMessage();
            await base.OnConnectedAsync();
		}

		public async Task SendMessage()
		{
			var queue = _context.queuemodels
				.Include(usrdet => usrdet.queueID)
				.ToListAsync();

            foreach (KeyValuePair<string, string> person in patients)
			{
				var id = _context.userdetails.FirstOrDefault(id => id.userReqID == person.Value);
				if(id != null)
				{
                    var ordernum = _context.queuemodels.FirstOrDefault(q => q.queueID == id.queueId);
                    if (ordernum != null)
                    {
                        string message = ordernum.queueordernum.ToString();
                        var user = person.Key;
                        await Clients.Client(user).SendAsync("MessageReceived", message);
                    }
                }
            }
        }
    }
}