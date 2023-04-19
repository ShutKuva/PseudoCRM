import { SecureSocketOptions } from "../enums/SecureSocketOptions";
import { ServerProtocols } from "../enums/ServerProtocols";

export interface ServerInformation {
  serverProtocol: number;
  server: string;
  port: number;
  secureSocketOptions: number;
}
