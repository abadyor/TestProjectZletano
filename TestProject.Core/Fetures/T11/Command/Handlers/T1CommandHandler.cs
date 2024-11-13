using AutoMapper;
using MediatR;
using System.Net;
using TestProject.Core.Fetures.T11.Command.Models;
using TestProject.Data.Entity;
using TestProject.Service.Abstract;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.T11.Command.Handlers
{
    public class T1CommandHandler : ResponseHandler,
        IRequestHandler<AddT1Command, Response<string>>,
        IRequestHandler<EditeT1Command, Response<string>>,
        IRequestHandler<DeleteT1Command, Response<string>>,
        IRequestHandler<CreateMultipleRecordsCommand, Response<string>>,
        IRequestHandler<EditeT1MultyCommand, Response<string>>,
        IRequestHandler<AddT1CreateRunTimeCommand, Response<string>>
    {
        private readonly IT1Service _t1Service;
        private readonly IMapper _mapper;

        public T1CommandHandler(IT1Service t1Service, IMapper mapper)
        {
            _t1Service = t1Service;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(AddT1Command request, CancellationToken cancellationToken)
        {


            var x = _mapper.Map<T1>(request);
            var result = await _t1Service.Create(x);
            if (result == "Success")
            {
                return Created("Add Successfuly");
            }
            else
            {
                return BadRequest<string>("لم تتم العملية من فضلك اعد المحاولة");
            }
        }

        public async Task<Response<string>> Handle(EditeT1Command request, CancellationToken cancellationToken)
        {
            var x = await _t1Service.GetByid(request.id);
            var response = new Response<string>();
            _mapper.Map(request, x);
            var result = await _t1Service.update(x);
            response.Data = result;
            response.Message = "Records update successfully.";
            response.Succeeded = true;
            response.StatusCode = HttpStatusCode.OK;

            return response;

            /*   var x = await _t1Service.GetByid(request.id);
               if (x == null)
               {
                   return NotFound<string>("student is not found");
               }

               _mapper.Map(request, x);

               var result = await _t1Service.update(x);
               if (result == "Success")
               {
                   return Created("Edite Successfuly");
               }
               else
               {
                   return BadRequest<string>("لم تتم العملية من فضلك اعد المحاولة");
               }*/

        }

        public async Task<Response<string>> Handle(DeleteT1Command request, CancellationToken cancellationToken)
        {

            var result = await _t1Service.Delete(request.id);



            if (result == "null")
            {
                return NotFound<string>("هذا العنصر غير موجود");
            }

            return Created("تمت العملية بنجاح");


            /*  return new Response<string>
          {
              Succeeded = result != "null",
              Message = result == "ok" ? "Record deleted successfully." : "Record not found.",
              StatusCode = result == "ok" ? 200 : 404 // OK أو Not Found
          };*/

            /* var x = await _t1Service.Delete(request.Id);
             if (x != "Success")
             {
                 return NotFound<string>("هدا العنصر غير موجود");

             }
             return Created("تمت العملية بنجاح");*/
        }

        public async Task<Response<string>> Handle(CreateMultipleRecordsCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();

            if (request.NumberOfRecords <= 0)
            {
                response.Errors.Add("Number of records must be greater than zero.");
                response.Succeeded = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }


            var result = await _t1Service.CreateMUlty(request.NumberOfRecords);
            response.Data = result;
            response.Message = "Records added successfully.";
            response.Succeeded = true;
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }

        public async Task<Response<string>> Handle(EditeT1MultyCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            var result = await _t1Service.UpdateMulty(request.fieldName);
            response.Data = result;
            response.Message = "Records update successfully.";
            response.Succeeded = true;
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }

        public async Task<Response<string>> Handle(AddT1CreateRunTimeCommand request, CancellationToken cancellationToken)
        {

            var result = await _t1Service.CreateTable(request.Table_Name);
            if (result == "okok")
            {
                return Created("Add Successfuly");
            }
            else
            {
                return BadRequest<string>("لم تتم العملية من فضلك اعد المحاولة");
            }
        }

        // دالة لعكس النص
        private string ReverseString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input; // إرجاع النص كما هو إذا كان فارغًا أو null

            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray); // عكس المصفوفة
            return new string(charArray); // إرجاع النص المعكوس
        }



    }
}

