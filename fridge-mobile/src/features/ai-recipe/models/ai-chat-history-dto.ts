export type AiChatHistoryDto = {
  id: number;
  role: string;
  userText?: string | null;
  aiPayloadJson?: string | null;
  createdAtUtc: string;
  expiresAtUtc: string;
};
