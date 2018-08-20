
                .AddMvcCore()
                    .AddApplicationPart(typeof(Controllers.V1.AppointmentsController).Assembly) // Fix for integration tests