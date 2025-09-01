﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MediatR;
using Refit;
using ProblemDetails =  Microsoft.AspNetCore.Mvc.ProblemDetails;


namespace UdemyNewMicroservice.Shared
{
	public interface IRequestByServiceResult<T>: IRequest<ServiceResult<T>>;
	public interface IRequestByServiceResult: IRequest<ServiceResult>;
	public class ServiceResult
	{
		// istek zaten bir statu döneceği için response a eklemeye gerek yok
		[JsonIgnore]
		public HttpStatusCode Status { get; set; }
		public ProblemDetails? Fail { get; set; }
		[JsonIgnore]
		public bool IsSuccess => Fail is null;
		[JsonIgnore]
		public bool IsFail => !IsSuccess;

		public static ServiceResult SuccessAsCreated()
		{
			return new ServiceResult
			{
				Status = HttpStatusCode.NoContent
			};
		}

		public static ServiceResult ErrorAsNotFound()
		{
			return new ServiceResult
			{
				Status = HttpStatusCode.NotFound,
				Fail = new ProblemDetails
				{
					Title = "Not Found",
					Detail = "The resource you are looking for could not be found."
				}
			};
		}

		public static ServiceResult ErrorFromProblemDetails(Refit.ApiException exception)
		{
			if (string.IsNullOrEmpty(exception.Content))
			{

				return new ServiceResult
				{
					Status = exception.StatusCode,
					Fail = new ProblemDetails()
					{
						Title = exception.Message,
						Detail = "No additional details provided."
					}
				};
			}
			var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content,
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});
			return new ServiceResult()
			{
				Fail = problemDetails,
				Status = exception.StatusCode
			};
		}

		public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode status)
		{
			return new ServiceResult
			{
				Status = status,
				Fail = problemDetails
			};
		}
		public static ServiceResult Error(string title, string description, HttpStatusCode status)
		{
			return new ServiceResult
			{
				Status = status,
				Fail = new ProblemDetails
				{
					Title = title,
					Detail = description,
					Status = status.GetHashCode()
				}
			};
		}

		public static ServiceResult ErrorFromValidation(IDictionary<string, object> errors)
		{
			return new ServiceResult()
			{
				Status = HttpStatusCode.BadRequest,
				Fail = new ProblemDetails()
				{
					Title = "Validation errors occured",
					Detail = "Please check the errors property for more details",
					Extensions = errors,
					Status = HttpStatusCode.BadRequest.GetHashCode()
				}
			};
		}
		public static ServiceResult ErrorFromValidation(string title)
		{
			return new ServiceResult()
			{
				Status = HttpStatusCode.BadRequest,
				Fail = new ProblemDetails()
				{
					Title = "Validation errors occured",
					Status = HttpStatusCode.BadRequest.GetHashCode()
				}
			};
		}

	}
	public class ServiceResult<T> : ServiceResult
	{
		public T? Data { get; set; }
		[JsonIgnore]public string UrlAsCreated { get; set; }
		public static ServiceResult<T> SuccessAsOk(T data)
		{
			return new ServiceResult<T>
			{
				Status = HttpStatusCode.OK,
				Data = data
			};
		}
		public static ServiceResult<T> SuccessAsCreated(T data, string url)
		{
			return new ServiceResult<T>
			{
				Status = HttpStatusCode.Created,
				Data = data,
				UrlAsCreated = url
			};
		}

		public new static ServiceResult<T> ErrorFromProblemDetails(Refit.ApiException exception)
		{
			if (string.IsNullOrEmpty(exception.Content))
			{

				return new ServiceResult<T>
				{
					Status = exception.StatusCode,
					Fail = new ProblemDetails()
					{
						Title = exception.Message,
						Detail = "No additional details provided."
					}
				};
			}
			var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content,
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});
			return new ServiceResult<T>()
			{
				Fail = problemDetails,
				Status = exception.StatusCode
			};
		}

		public new static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode status)
		{
			return new ServiceResult<T>
			{
				Status = status,
				Fail = problemDetails
			};
		}
		public new static ServiceResult<T> Error(string title, string description, HttpStatusCode status)
		{
			return new ServiceResult<T>
			{
				Status = status,
				Fail = new ProblemDetails
				{
					Title = title,
					Detail = description,
					Status = status.GetHashCode()
				}
			};
		}

		public new static ServiceResult<T> ErrorFromValidation(IDictionary<string, object> errors)
		{
			return new ServiceResult<T>()
			{
				Status = HttpStatusCode.BadRequest,
				Fail = new ProblemDetails()
				{
					Title = "Validation errors occured",
					Detail = "Please check the errors property for more details",
					Extensions = errors,
					Status = HttpStatusCode.BadRequest.GetHashCode()
				}
			};
		}
		public new static ServiceResult<T> ErrorFromValidation(string title)
		{
			return new ServiceResult<T>()
			{
				Status = HttpStatusCode.BadRequest,
				Fail = new ProblemDetails()
				{
					Title = "Validation errors occured",
					Status = HttpStatusCode.BadRequest.GetHashCode()
				}
			};
		}
	}
}

