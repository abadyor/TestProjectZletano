using AutoMapper;
using MediatR;
using System.Text.Json;
using TestProject.Core.Fetures.ControlTables.Commands.Models;
using TestProject.Data.Entity;
using TestProject.Service.Abstract;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.ControlTables.Commands.Handles
{
    public class ControlCommandHandler : ResponseHandler,
                                        IRequestHandler<AddControlCommand, Response<string>>,
                                        IRequestHandler<AddDynamicTableCommand, Response<string>>
    {
        private readonly IControlService _controlService;
        private readonly IMapper _mapper;
        public ControlCommandHandler(IControlService controlService, IMapper mapper)
        {
            _mapper = mapper;
            _controlService = controlService;
        }
        public async Task<Response<string>> Handle(AddControlCommand request, CancellationToken cancellationToken)
        {

            var lastStoreCode = await _controlService.GetLastControlLastCode();

            if (lastStoreCode != "000")
            {


                var controltable = new ControlTable
                {
                    id_vendor = request.id_vendor,
                    M_Code = request.M_Code,
                    Last_sore = lastStoreCode,
                    shopeName = request.shopeName,
                    Address = request.Address,
                    region = request.region,
                    city = request.city,
                    Street = request.Street,
                    NerestPoint = request.NerestPoint,
                    visitor = 0
                };

                var issAdd = await _controlService.AddAsync(controltable);
                if (issAdd)
                {
                    var nameTable = request.M_Code + request.city + lastStoreCode;
                    // var insertocontrol = await _vendorRepository.InsertIntoTableAsync(region, lastStoreCode.ToString(), 0, vendor.shopeName, vendor.Street);
                    var create = await _controlService.CreateTable(nameTable);
                    if (create == "Ok Create")
                    {
                        return Success("ok Create");
                    }
                    else
                    {
                        return Success("No Create");
                    }



                }
                else
                {
                    return BadRequest<string>("No Add");
                }


            }
            else
            {
                return BadRequest<string>("The Code Is Wronge");
            }


        }

        public async Task<Response<string>> Handle(AddDynamicTableCommand request, CancellationToken cancellationToken)
        {
            // تحقق من أن اسم الجدول صالح
            /*    if (string.IsNullOrEmpty(request.TableName) || !Regex.IsMatch(request.TableName, @"^s_\d+$"))
                {
                    return new Response<string>("Invalid table name format.", false);
                }

                // استدعاء الخدمة لإضافة البيانات إلى الجدول الديناميكي
                try
                {
                    await _controlService.AddItemToTable(request.TableName, request.Fields);
                    return new Response<string>("Data added successfully!", true);
                }
                catch (Exception ex)
                {
                    return new Response<string>($"Error occurred: {ex.Message}", false);
                }*/

            // تعديل تعبير Regex
            /*   if (string.IsNullOrEmpty(request.TableName) || !Regex.IsMatch(request.TableName, @"^s_\d+$"))
               {
                   return new Response<string>("Invalid table name format.", false);
               }

               // تابع باقي الكود كما هو
               var convertedData = ConvertJsonElements(request.Fields);

               try
               {
                   await _controlService.AddItemToTable(request.TableName, convertedData);
                   return new Response<string>("Data added successfully!", true);
               }
               catch (Exception ex)
               {
                   return new Response<string>($"Error occurred: {ex.Message}", false);
               }*/


            var data = new Dictionary<string, object>
        {
            { "Item", request.Item },
            { "Discription", request.Discription },
            { "Price", request.Price },
            { "Quantity", request.Quantity },
            { "CountImage", request.CountImage },
            { "NameImage", request.NameImage }
        };

            await _controlService.AddItemToTable(request.TableName, data);

            return new Response<string>
            {
                Data = "Item added successfully.",
                Succeeded = true
            };


        }


        private Dictionary<string, object> ConvertJsonElements(Dictionary<string, object> originalData)
        {
            var convertedData = new Dictionary<string, object>();

            foreach (var kvp in originalData)
            {
                if (kvp.Value is JsonElement jsonElement)
                {
                    convertedData[kvp.Key] = ConvertJsonElement(jsonElement);
                }
                else
                {
                    convertedData[kvp.Key] = kvp.Value;
                }
            }

            return convertedData;
        }


        private object ConvertJsonElement(JsonElement jsonElement)
        {
            return jsonElement.ValueKind switch
            {
                JsonValueKind.String => jsonElement.GetString(),
                JsonValueKind.Number => jsonElement.TryGetInt32(out int i) ? i : jsonElement.GetDecimal(),
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                _ => jsonElement.GetRawText()  // القيمة كـ نص خام في حال كانت غير معرفة
            };
        }
    }
}
