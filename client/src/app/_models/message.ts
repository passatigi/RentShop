
export interface Message {
    id: number
    senderId: number
    recipientId: number
    orderId: number
    content: string
    isRead: boolean
    MessageSent: Date
}