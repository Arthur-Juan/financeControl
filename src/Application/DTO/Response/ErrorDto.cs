namespace Application.DTO.Response;


public record ErrorDto(
    int StatusCode,
    string ErrorMessage
);