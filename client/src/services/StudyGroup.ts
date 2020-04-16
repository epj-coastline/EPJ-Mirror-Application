import User from "@/services/User";
import Module from "@/services/Module";

interface StudyGroup {
  id: number;
  purpose: string;
  creationDate: Date;
  creator: User;
  module: Module;
}

export default StudyGroup;
