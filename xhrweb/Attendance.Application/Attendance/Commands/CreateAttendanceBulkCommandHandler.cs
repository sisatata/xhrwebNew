using ASL.Hrms.SharedKernel.Interfaces;
using Attendance.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;
using AttendanceEntities = Attendance.Core.Entities;
using System.Linq;

namespace Attendance.Application.Attendance.Commands
{
    public class CreateAttendanceBulkCommandHandler : IRequestHandler<Commands.V1.CreateAttendanceBulk, AttendanceCommandVM>
    {
        private readonly IAsyncRepository<AttendanceEntities.Attendance1, Guid> _repository1;
        private readonly IAsyncRepository<AttendanceEntities.Attendance2, Guid> _repository2;
        private readonly IAsyncRepository<AttendanceEntities.Attendance3, Guid> _repository3;
        private readonly IAsyncRepository<AttendanceEntities.Attendance4, Guid> _repository4;
        private readonly IAsyncRepository<AttendanceEntities.Attendance5, Guid> _repository5;
        private readonly IAsyncRepository<AttendanceEntities.Attendance6, Guid> _repository6;
        private readonly IAsyncRepository<AttendanceEntities.Attendance7, Guid> _repository7;
        private readonly IAsyncRepository<AttendanceEntities.Attendance8, Guid> _repository8;
        private readonly IAsyncRepository<AttendanceEntities.Attendance9, Guid> _repository9;
        private readonly IAsyncRepository<AttendanceEntities.Attendance10, Guid> _repository10;
        private readonly IAsyncRepository<AttendanceEntities.Attendance11, Guid> _repository11;
        private readonly IAsyncRepository<AttendanceEntities.Attendance12, Guid> _repository12;
        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;
        public CreateAttendanceBulkCommandHandler(IAsyncRepository<AttendanceEntities.Attendance1, Guid> repository1,
            IAsyncRepository<AttendanceEntities.Attendance2, Guid> repository2,
            IAsyncRepository<AttendanceEntities.Attendance3, Guid> repository3,
            IAsyncRepository<AttendanceEntities.Attendance4, Guid> repository4,
            IAsyncRepository<AttendanceEntities.Attendance5, Guid> repository5,
            IAsyncRepository<AttendanceEntities.Attendance6, Guid> repository6,
            IAsyncRepository<AttendanceEntities.Attendance7, Guid> repository7,
            IAsyncRepository<AttendanceEntities.Attendance8, Guid> repository8,
            IAsyncRepository<AttendanceEntities.Attendance9, Guid> repository9,
            IAsyncRepository<AttendanceEntities.Attendance10, Guid> repository10,
            IAsyncRepository<AttendanceEntities.Attendance11, Guid> repository11,
            IAsyncRepository<AttendanceEntities.Attendance12, Guid> repository12,
            IServiceBus serviceBus,
            IConfiguration configuration
            )
        {
            _repository1 = repository1;
            _repository2 = repository2;
            _repository3 = repository3;
            _repository4 = repository4;
            _repository5 = repository5;
            _repository6 = repository6;
            _repository7 = repository7;
            _repository8 = repository8;
            _repository9 = repository9;
            _repository10 = repository10;
            _repository11 = repository11;
            _repository12 = repository12;
            _serviceBus = serviceBus;
            _configuration = configuration;

        }


        //public CreateCommandHandler(IAsyncRepository<AttendanceEntities.Attendance2, Guid> repository) { _repository2 = repository; }

        public async Task<AttendanceCommandVM> Handle(Commands.V1.CreateAttendanceBulk requestBulk, CancellationToken cancellationToken)
        {
            var response = new AttendanceCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                foreach (var request in requestBulk.CreateAttendanceList)
                {


                    switch (request.AttendanceDate.Value.Month)
                    {    //January
                        case 1:
                            var entity1 = AttendanceEntities.Attendance1.Create(
                             request.CardId,
                             request.EmployeeId,
                             request.AttendanceYear,
                             request.AttendanceDate,
                             request.Punchtype,
                             request.OverNightMark,
                             request.Latitude,
                             request.Longitude,
                             request.IsDeleted,
                             request.ClockTimeType,
                             request.Remarks,
                             request.ClockTimeAddress
                    );
                            var data = await _repository1.AddAsync(entity1);
                            response.Id = entity1.Id;
                            break;

                        //February
                        case 2:
                            var entity2 = AttendanceEntities.Attendance2.Create(
                             request.CardId,
                             request.EmployeeId,
                             request.AttendanceYear,
                             request.AttendanceDate,
                             request.Punchtype,
                             request.OverNightMark,
                             request.Latitude,
                             request.Longitude,
                             request.IsDeleted,
                             request.ClockTimeType,
                             request.Remarks,
                             request.ClockTimeAddress
                    );
                            var data2 = await _repository2.AddAsync(entity2);
                            response.Id = entity2.Id;
                            break;


                        //March
                        case 3:
                            var entity3 = AttendanceEntities.Attendance3.Create(
                             request.CardId,
                             request.EmployeeId,
                             request.AttendanceYear,
                             request.AttendanceDate,
                             request.Punchtype,
                             request.OverNightMark,
                             request.Latitude,
                             request.Longitude,
                             request.IsDeleted,
                             request.ClockTimeType,
                             request.Remarks,
                             request.ClockTimeAddress
                    );
                            var data3 = await _repository3.AddAsync(entity3);
                            response.Id = entity3.Id;
                            break;


                        //April
                        case 4:
                            var entity4 = AttendanceEntities.Attendance4.Create(
                             request.CardId,
                             request.EmployeeId,
                             request.AttendanceYear,
                             request.AttendanceDate,
                             request.Punchtype,
                             request.OverNightMark,
                             request.Latitude,
                             request.Longitude,
                             request.IsDeleted,
                             request.ClockTimeType,
                             request.Remarks,
                             request.ClockTimeAddress
                    );
                            var data4 = await _repository4.AddAsync(entity4);
                            response.Id = entity4.Id;
                            break;


                        //May
                        case 5:
                            var entity5 = AttendanceEntities.Attendance5.Create(
                             request.CardId,
                             request.EmployeeId,
                             request.AttendanceYear,
                             request.AttendanceDate,
                             request.Punchtype,
                             request.OverNightMark,
                             request.Latitude,
                             request.Longitude,
                             request.IsDeleted,
                             request.ClockTimeType,
                             request.Remarks,
                             request.ClockTimeAddress
                    );
                            var data5 = await _repository5.AddAsync(entity5);
                            response.Id = entity5.Id;
                            break;


                        //June
                        case 6:
                            var entity6 = AttendanceEntities.Attendance6.Create(
                             request.CardId,
                             request.EmployeeId,
                             request.AttendanceYear,
                             request.AttendanceDate,
                             request.Punchtype,
                             request.OverNightMark,
                             request.Latitude,
                             request.Longitude,
                             request.IsDeleted,
                             request.ClockTimeType,
                             request.Remarks,
                             request.ClockTimeAddress
                    );
                            var data6 = await _repository6.AddAsync(entity6);
                            response.Id = entity6.Id;
                            break;


                        //July
                        case 7:
                            var entity7 = AttendanceEntities.Attendance7.Create(
                             request.CardId,
                             request.EmployeeId,
                             request.AttendanceYear,
                             request.AttendanceDate,
                             request.Punchtype,
                             request.OverNightMark,
                             request.Latitude,
                             request.Longitude,
                             request.IsDeleted,
                             request.ClockTimeType,
                             request.Remarks,
                             request.ClockTimeAddress
                    );
                            var data7 = await _repository7.AddAsync(entity7);
                            response.Id = entity7.Id;
                            break;


                        //August
                        case 8:
                            var entity8 = AttendanceEntities.Attendance8.Create(
                             request.CardId,
                             request.EmployeeId,
                             request.AttendanceYear,
                             request.AttendanceDate,
                             request.Punchtype,
                             request.OverNightMark,
                             request.Latitude,
                             request.Longitude,
                             request.IsDeleted,
                             request.ClockTimeType,
                             request.Remarks,
                             request.ClockTimeAddress
                    );
                            var data8 = await _repository8.AddAsync(entity8);
                            response.Id = entity8.Id;
                            break;


                        //September
                        case 9:
                            var entity9 = AttendanceEntities.Attendance9.Create(
                             request.CardId,
                             request.EmployeeId,
                             request.AttendanceYear,
                             request.AttendanceDate,
                             request.Punchtype,
                             request.OverNightMark,
                             request.Latitude,
                             request.Longitude,
                             request.IsDeleted,
                             request.ClockTimeType,
                             request.Remarks,
                             request.ClockTimeAddress
                    );
                            var data9 = await _repository9.AddAsync(entity9);
                            response.Id = entity9.Id;
                            break;


                        //October
                        case 10:
                            var entity10 = AttendanceEntities.Attendance10.Create(
                             request.CardId,
                             request.EmployeeId,
                             request.AttendanceYear,
                             request.AttendanceDate,
                             request.Punchtype,
                             request.OverNightMark,
                             request.Latitude,
                             request.Longitude,
                             request.IsDeleted,
                             request.ClockTimeType,
                             request.Remarks,
                             request.ClockTimeAddress
                    );
                            var data10 = await _repository10.AddAsync(entity10);
                            response.Id = entity10.Id;
                            break;


                        //Nevember
                        case 11:
                            var entity11 = AttendanceEntities.Attendance11.Create(
                             request.CardId,
                             request.EmployeeId,
                             request.AttendanceYear,
                             request.AttendanceDate,
                             request.Punchtype,
                             request.OverNightMark,
                             request.Latitude,
                             request.Longitude,
                             request.IsDeleted,
                             request.ClockTimeType,
                             request.Remarks,
                             request.ClockTimeAddress
                    );
                            var data11 = await _repository11.AddAsync(entity11);
                            response.Id = entity11.Id;
                            break;


                        //December
                        case 12:
                            var entity12 = AttendanceEntities.Attendance12.Create(
                             request.CardId,
                             request.EmployeeId,
                             request.AttendanceYear,
                             request.AttendanceDate,
                             request.Punchtype,
                             request.OverNightMark,
                             request.Latitude,
                             request.Longitude,
                             request.IsDeleted,
                             request.ClockTimeType,
                             request.Remarks,
                             request.ClockTimeAddress
                    );
                            var data12 = await _repository12.AddAsync(entity12);
                            response.Id = entity12.Id;
                            break;
                        default:
                            break;
                    }
                }
                if (requestBulk.CreateAttendanceList != null && requestBulk.CreateAttendanceList.Count > 0)
                {
                    var @event = new Core.Events.AttendanceProcessEvent
                    {
                        EmployeeId = requestBulk.CreateAttendanceList[0].EmployeeId,

                        CommandPublished = DateTime.Now,
                        StartDate = requestBulk.CreateAttendanceList.Min(x => x.AttendanceDate).Value.Date,
                        EndDate = requestBulk.CreateAttendanceList.Max(x => x.AttendanceDate).Value.Date
                    };

                    await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);
                }
                response.Status = true;
                response.Message = "entity created successfully.";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
