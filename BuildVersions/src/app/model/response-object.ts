export interface ResponseObject<T> {
  succeeded: boolean;
  message: string;
  data: T;
}

