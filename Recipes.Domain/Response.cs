using System;

namespace Recipes.Domain
{
  public class Response<T>
  {
    public int HttpStatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public Response(T data, int httpStatusCode, string message = null)
    {
      Data = data;
      Message = message;

      if(typeof(T) == typeof(System.Exception))
      {
        HttpStatusCode = 500;
      }
      else
      {
        HttpStatusCode = httpStatusCode;
      }
    }
  }
}