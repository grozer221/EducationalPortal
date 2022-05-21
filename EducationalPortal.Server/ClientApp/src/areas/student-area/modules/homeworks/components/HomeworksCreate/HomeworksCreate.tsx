import React, {ChangeEvent, FC, useState} from 'react';
import {useMutation} from '@apollo/client';
import {Form, Input, message} from 'antd';
import {ButtonCreate} from '../../../../../../components/ButtonCreate/ButtonCreate';
import {sizeButtonItem, sizeFormItem} from '../../../../../../styles/form';
import Title from 'antd/es/typography/Title';
import {
    CREATE_HOMEWORK_MUTATION,
    CreateHomeworkData,
    CreateHomeworkVars,
} from '../../../../../../gql/modules/homeworks/homeworks.mutations';

type Props = {
    subjectPostId: string,
    subjectPostTitle: string,
    afterCreate: () => void,
}

type FormValues = {
    text: string,
}

export const HomeworksCreate: FC<Props> = ({subjectPostId, subjectPostTitle, afterCreate}) => {
    const [createHomework, createHomeworkOption] = useMutation<CreateHomeworkData, CreateHomeworkVars>(CREATE_HOMEWORK_MUTATION);
    const [form] = Form.useForm();
    const [files, setFiles] = useState<File[]>([]);

    const filesChangeHandler = (e: ChangeEvent<HTMLInputElement>) => {
        if (e.target.files?.length) {
            console.log(e.target.files);
            const newFiles = Array.from(e.target.files)
            setFiles([...files, ...newFiles]);
        }
    };

    const onFinish = async ({text}: FormValues) => {
        console.log('upload', files);
        createHomework({
            variables: {
                createHomeworkInputType: {
                    text,
                    subjectPostId,
                    files,
                },
            },
        })
            .then(response => {
                message.success(`ДЗ для ${subjectPostTitle} було відпралено`);
                afterCreate();
            })
            .catch(error => {
                message.error(error.message);
            });
    };

    return (
        <Form
            name="HomeworksCreateForm"
            onFinish={onFinish}
            form={form}
            {...sizeFormItem}
        >
            <Title level={2}>Створити ДЗ для {subjectPostTitle}</Title>
            <Form.Item
                name="text"
                label="Текст"
                rules={[{required: true, message: 'Введіть текст!'}]}
            >
                <Input placeholder="Назва"/>
            </Form.Item>
            <Form.Item
                name="files"
                label="Файли"
            >
                <input type={'file'} onChange={filesChangeHandler}/>
            </Form.Item>
            <Form.Item {...sizeButtonItem}>
                <ButtonCreate loading={createHomeworkOption.loading} isSubmit={true}/>
            </Form.Item>
        </Form>
    );
};
