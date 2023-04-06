export interface EmailTextMessage {
  to: string[];
  from: string[];
  subject?: string;
  text: string;
}
