using AutoMapper;
using MediatR;
using System.Diagnostics;
using TestProject.Core.Fetures.T11.Queries.Models;
using TestProject.Core.Fetures.T11.Queries.Response;
using TestProject.Data.Entity;
using TestProject.Service.Abstract;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.T11.Queries.Handlers
{
    public class T1HandlerQueries : ResponseHandler,
        IRequestHandler<GetT1ListQueries, Response<List<GetT1Response>>>,
        IRequestHandler<GetT1ByIdQuery, Response<GetT1ByIdResponse>>,
        IRequestHandler<GetT1ListTimeQueries, (List<GetT1TimeResponse> records, long elapsedMilliseconds)>,
        IRequestHandler<GetDataTimeWhereRecordQuery, (List<T1> records, long elapsedMilliseconds)>
    {


        #region Field
        private readonly IT1Service _t1Service;

        private readonly IMapper _mapper;
        #endregion
        #region Constractor

        public T1HandlerQueries(IT1Service t1Service, IMapper mapper)
        {
            _t1Service = t1Service;
            _mapper = mapper;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<List<GetT1Response>>> Handle(GetT1ListQueries request, CancellationToken cancellationToken)
        {
            //var x = await _t1Service.GetDataAll();
            var x = await _t1Service.GetDataAll();
            var xListMapper = _mapper.Map<List<GetT1Response>>(x);
            return Success(xListMapper);
        }

        public async Task<Response<GetT1ByIdResponse>> Handle(GetT1ByIdQuery request, CancellationToken cancellationToken)
        {
            var x = await _t1Service.GetByid(request.id);
            if (x == null)
            {
                return NotFound<GetT1ByIdResponse>("Id Not Exist");
            }
            var xByidMapper = _mapper.Map<GetT1ByIdResponse>(x);
            return Success(xByidMapper);
        }

        public async Task<(List<GetT1TimeResponse> records, long elapsedMilliseconds)> Handle(GetT1ListTimeQueries request, CancellationToken cancellationToken)
        {


            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // استدعاء الخدمة للحصول على البيانات
            var (records, elapsedMillisecondsFromService) = await _t1Service.GetDataAllTime();

            // تحويل السجلات
            var xListMapper = _mapper.Map<List<GetT1TimeResponse>>(records);

            stopwatch.Stop();
            var totalElapsedMilliseconds = elapsedMillisecondsFromService + stopwatch.ElapsedMilliseconds;

            return (xListMapper, totalElapsedMilliseconds);


        }

        public async Task<(List<T1> records, long elapsedMilliseconds)> Handle(GetDataTimeWhereRecordQuery request, CancellationToken cancellationToken)

        {
            if (request.number <= 0)
            {
                return (new List<T1>(), 0); // إرجاع قائمة فارغة ووقت استجابة 0
            }
            // var stopwatch = new Stopwatch();
            var (records, elapsedMilliseconds) = await _t1Service.GetDataTimeWhereRecord(request.number); // تأكد من استخدام الخصائص الصحيحة

            // إرجاع السجلات ووقت الاستجابة
            return (records, elapsedMilliseconds);

            /*  var stopwatch = new Stopwatch();
              stopwatch.Start();
              return await _t1Service.GetDataTimeWhereRecord(request.number);*/
        }


        #endregion
    }
}
