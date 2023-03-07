namespace WebApiProduccion.Models
{
	public class myResponse
	{
		public bool IsSucces { get; set; }

		public Object Result { get; set; } = true;

		public string DisplayMessage { get; set; }

		public List<string> ErrorMessages { get; set; }
	}
}
