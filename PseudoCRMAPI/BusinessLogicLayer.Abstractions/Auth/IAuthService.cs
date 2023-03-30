namespace BusinessLogicLayer.Abstractions.Auth
{
    public interface IAuthService<TLogin, TRegister, TLoginResult, TRegisterResult>
    {
        public Task<TLoginResult> Login(TLogin parameters);
        public Task<TRegisterResult> Register(TRegister parameters);
    }
}