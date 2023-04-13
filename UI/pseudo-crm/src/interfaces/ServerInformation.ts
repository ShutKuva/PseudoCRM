import { SecureSocketOptions } from "../enums/SecureSocketOptions";
import { ServerProtocols } from "../enums/ServerProtocols";

export interface ServerInformation {
  serverProtocol: ServerProtocols;
  server: string;
  port: number;
  secureSocketOptions: SecureSocketOptions;
}
