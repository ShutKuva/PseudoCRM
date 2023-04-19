export interface OAuthConfig {
  name: string;
  authorizeEnd: string;
  tokenEnd: string;
  clientId: string;
  clientSecret: string;
  scope: string;
  responseType: string;
}

export interface OAuthState {
  state?: string;
  code?: string;
}

export interface OAuthAuthRequest {
  redirectUri: string;
  state: string;
}

export interface OAuthAuthResponse {
  code: string;
}

export interface OAuthTokenRequest {
  redirectUri: string;
  code: string;
}

export interface OAuthTokenResponse {
  access_token: string;
  expires_in: number;
  refresh_token: string;
}
